using System.Globalization;
using PrestoQServicesEncoding;
using Xunit;

namespace PrestoQServicesEncodingTest
{
    public class ProductCatalogReaderTest
    {
        [Fact]
        public void ReadField()
        {
            string value = ProductCatalogReader.ReadField("xxxx This is the destination string     yyyy", 6, 40);
            Assert.Equal("This is the destination string", value);
        }

        [Fact]
        public void ParseString()
        {
            var parsed = ProductCatalogReader.ParseField("This is a string", InputType.String);
            Assert.IsType<string>(parsed);
            Assert.Equal("This is a string", parsed);
        }

        [Fact]
        public void ParseNumber()
        {
            var parsed = ProductCatalogReader.ParseField("00001234", InputType.Number);
            Assert.IsType<int>(parsed);
            Assert.Equal(1234, parsed);
        }

        [Fact]
        public void ParseCurrency()
        {
            var parsedPositive = ProductCatalogReader.ParseField("00123456", InputType.Currency);
            Assert.IsType<decimal>(parsedPositive);
            Assert.Equal(1234.56m, parsedPositive);

            var parsedNegative = ProductCatalogReader.ParseField("-0123456", InputType.Currency);
            Assert.IsType<decimal>(parsedNegative);
            Assert.Equal(-1234.56m, parsedNegative);
        }

        [Fact]
        public void ParseFlags()
        {
            var parsed = ProductCatalogReader.ParseField("YNNY", InputType.Flags);
            Assert.IsType<bool[]>(parsed);

            bool[] flags = (bool[])parsed;
            Assert.Equal(4, flags.Length);

            bool[] expected = new bool[] { true, false, false, true };
            Assert.Equal<bool>(expected, flags);
        }

        [Fact]
        public void RoundHalfDown()
        {
            Assert.Equal(12.34m, ProductCatalogReader.RoundHalfDown(12.34199m, 2));
            Assert.Equal(12.34m, ProductCatalogReader.RoundHalfDown(12.345m, 2));
            Assert.Equal(12.35m, ProductCatalogReader.RoundHalfDown(12.346m, 2));
        }

        [Fact]
        public void ParseCommonFields()
        {
            const string line =
                "80000001 Kimchi-flavored white rice                                  00000567 00000000 00000000 00000000 00000000 00000000 NNNNNNNNN      18oz";

            var record = ProductCatalogReader.Parse(line);
            Assert.Equal(80000001, record.Id);
            Assert.Equal("Kimchi-flavored white rice", record.Description);
            Assert.Equal("18oz", record.Size);
            Assert.Equal(UnitOfMeasure.Each, record.UnitOfMeasure);
            Assert.Equal(0, record.TaxRate);
        }

        [Fact]
        public void ParseRegularSingularPrice()
        {
            const string line =
                "80000001 Kimchi-flavored white rice                                  00000567 00000000 00000000 00000000 00000000 00000000 NNNNNNNNN      18oz";

            var record = ProductCatalogReader.Parse(line);
            Assert.Equal("$5.67", record.RegularDisplayPrice);
            Assert.Equal(5.67m, record.RegularCalculatorPrice);
        }

        [Fact]
        public void ParseRegularForEachPrice()
        {
            const string line =
                "14963801 Generic Soda 12-pack                                        00000000 00000549 00001300 00000000 00000002 00000000 NNNNYNNNN   12x12oz";

            var record = ProductCatalogReader.Parse(line);
            Assert.Equal("2 for $13.00", record.RegularDisplayPrice);
            Assert.Equal(6.50m, record.RegularCalculatorPrice);
        }

        [Fact]
        public void ParsePromotionalSingularPrice()
        {
            const string line =
                "14963801 Generic Soda 12-pack                                        00000000 00000549 00001300 00000000 00000002 00000000 NNNNYNNNN   12x12oz";

            var record = ProductCatalogReader.Parse(line);
            Assert.Equal("$5.49", record.PromotionalDisplayPrice);
            Assert.Equal(5.49m, record.PromotionalCalculatorPrice);
        }

        [Fact]
        public void ParsePromotionalForEachPrice()
        {
            const string line =
                "12345678 Test product                                                00000600 00000000 00000000 00001500 00000000 00000003 NNNNYNNNN          ";

            var record = ProductCatalogReader.Parse(line);
            Assert.Equal("3 for $15.00", record.PromotionalDisplayPrice);
            Assert.Equal(5.00m, record.PromotionalCalculatorPrice);
        }

        [Fact]
        public void ParsePerWeightItem()
        {
            const string line =
                "50133333 Fuji Apples (Organic)                                       00000349 00000000 00000000 00000000 00000000 00000000 NNYNNNNNN        lb";

            var record = ProductCatalogReader.Parse(line);
            Assert.Equal(UnitOfMeasure.Pound, record.UnitOfMeasure);
        }

        [Fact]
        public void ParseTaxable()
        {
            const string line =
                "14963801 Generic Soda 12-pack                                        00000000 00000549 00001300 00000000 00000002 00000000 NNNNYNNNN   12x12oz";

            var record = ProductCatalogReader.Parse(line);
            Assert.Equal(0.07775m, record.TaxRate);
        }
    }
}
