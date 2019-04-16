namespace PrestoQServicesEncoding
{
    public class ProductRecord
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string RegularDisplayPrice { get; set; }
        public decimal RegularCalculatorPrice { get; set; }
        public string PromotionalDisplayPrice { get; set; }
        public decimal PromotionalCalculatorPrice { get; set; }
        public UnitOfMeasure UnitOfMeasure { get; set; }
        public string Size { get; set; }
        public decimal TaxRate { get; set; }
    }
}
