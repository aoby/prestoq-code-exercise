using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace PrestoQServicesEncoding
{
    public class ProductCatalogReader
    {
        private StreamReader reader;
        public const decimal TAX_RATE = 0.07775m; // 7.775 %

        private static CultureInfo cultureInfo = new CultureInfo("en-US");

        public ProductCatalogReader(string fileName)
        {
            this.reader = new StreamReader(fileName);
        }

        public IEnumerable<ProductRecord> ReadLines()
        {
            ProductRecord productRecord;
            while ((productRecord = ReadLine()) != null)
            {
                yield return productRecord;
            }
        }

        public ProductRecord ReadLine()
        {
            string line = this.reader.ReadLine();
            if (line == null)
            {
                return null;
            }

            return Parse(line);
        }

        public static ProductRecord Parse(string line)
        {
            var productRecord = new ProductRecord()
            {
                Id = ReadField<int>(line, ProductCatalogField.ProductId),
                Description = ReadField<string>(line, ProductCatalogField.ProductDescription),
                Size = ReadField<string>(line, ProductCatalogField.ProductSize),
            };

            var regularSingularPrice = ReadField<decimal>(line, ProductCatalogField.RegularSinglarPrice);
            if (regularSingularPrice > 0)
            {
                productRecord.RegularDisplayPrice = regularSingularPrice.ToString("C", cultureInfo);
                productRecord.RegularCalculatorPrice = regularSingularPrice;
            } else
            {
                var regularSplitPrice = ReadField<decimal>(line, ProductCatalogField.RegularSplitPrice);
                var regularForX = ReadField<int>(line, ProductCatalogField.RegularForX);

                productRecord.RegularDisplayPrice = $"{regularForX} for {regularSplitPrice.ToString("C", cultureInfo)}";
                productRecord.RegularCalculatorPrice = RoundHalfDown(regularSplitPrice / regularForX, 4);
            }

            var promotionalSingularPrice = ReadField<decimal>(line, ProductCatalogField.PromotionalSinglarPrice);
            var promotionalSplitPrice = ReadField<decimal>(line, ProductCatalogField.PromotionalSplitPrice);
            if (promotionalSingularPrice > 0)
            {
                productRecord.PromotionalDisplayPrice = promotionalSingularPrice.ToString("C", cultureInfo);
                productRecord.PromotionalCalculatorPrice = promotionalSingularPrice;
            } else if (promotionalSplitPrice > 0) {
                var promotionalForX = ReadField<int>(line, ProductCatalogField.PromotionalForX);

                productRecord.PromotionalDisplayPrice = $"{promotionalForX} for {promotionalSplitPrice.ToString("C", cultureInfo)}";
                productRecord.PromotionalCalculatorPrice = RoundHalfDown(promotionalSplitPrice / promotionalForX, 4);
            }

            var flags = ReadField<bool[]>(line, ProductCatalogField.Flags);
            bool isWeighted = flags[2]; // Flag #3 (index 2)
            bool isTaxable = flags[4]; // Flag #5 (index 4)
            productRecord.UnitOfMeasure = isWeighted ? UnitOfMeasure.Pound : UnitOfMeasure.Each;
            productRecord.TaxRate = isTaxable ? TAX_RATE : 0;

            return productRecord;
        }

        public static T ReadField<T>(string line, ProductCatalogField field)
        {
            var value = ReadField(line, field.Start, field.End);
            return (T)ParseField(value, field.InputType);
        }

        public static object ParseField(string value, InputType inputType)
        {
            switch(inputType)
            {
                case InputType.String:
                    return value;
                case InputType.Number:
                    return (int)Convert.ChangeType(value, typeof(int));
                case InputType.Currency:
                    return ((decimal)Convert.ChangeType(value, typeof(decimal))) / 100m;
                case InputType.Flags:
                    bool[] flags = new bool[value.Length];
                    for (int i = 0; i < value.Length; i++)
                    {
                        // Assume it's 'N' when it's not 'Y'. No need for error checking, per spec.
                        flags[i] = value[i] == 'Y';
                    }
                    return flags;
                default:
                    throw new ArgumentException($"Unexpected input type: {inputType}", nameof(inputType));
            }
        }

        public static string ReadField(string line, int start, int end)
        {
            // start is 1-based; line is 0-based
            int startIndex = start - 1;
            int length = end - startIndex;

            return line.Substring(startIndex, length).Trim();
        }

        // Math.Round only supports AwayFromZero (up for positive numbers) and ToEven rounding, but not down.
        public static decimal RoundHalfDown(decimal value, int precision)
        {
            decimal factor = (decimal)Math.Pow(10, precision);
            decimal expanded = value * factor;
            if (expanded % 0.5m == 0)
            {
                // Half? Round down (i.e. floor)
                return Math.Floor(expanded) / factor;
            }

            // Not half? Round normally
            return Math.Round(expanded) / factor;
        }
    }
}
