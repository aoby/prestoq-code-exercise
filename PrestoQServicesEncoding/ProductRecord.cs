namespace PrestoQServicesEncoding
{
    /// <summary>
    /// Product record.
    /// </summary>
    public class ProductRecord
    {
        /// <summary>
        /// Product Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Product description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Regular display price as an English-readable string
        /// </summary>
        public string RegularDisplayPrice { get; set; }

        /// <summary>
        /// Regular Calculator Price
        /// </summary>
        /// <remarks>
        /// Price the calculator should use, rounded to 4 decimal places, half-down
        /// </remarks>
        public decimal RegularCalculatorPrice { get; set; }

        /// <summary>
        /// Promotional display price as an English-readable string, if it exists
        /// </summary>
        public string PromotionalDisplayPrice { get; set; }

        /// <summary>
        /// Promotional Calculator Price, if it exists
        /// </summary>
        /// <remarks>
        /// Price the calculator should use, rounded to 4 decimal places, half-down
        /// </remarks>
        public decimal? PromotionalCalculatorPrice { get; set; }

        /// <summary>
        /// Unit of measure, "Each" or "Pound"
        /// </summary>
        public UnitOfMeasure UnitOfMeasure { get; set; }

        /// <summary>
        /// Product size (English string)
        /// </summary>
        public string Size { get; set; }

        /// <summary>
        /// Tax rate
        /// </summary>
        public decimal TaxRate { get; set; }
    }
}
