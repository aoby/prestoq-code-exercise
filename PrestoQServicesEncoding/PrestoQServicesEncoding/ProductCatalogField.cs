using System;

namespace PrestoQServicesEncoding
{
    public class ProductCatalogField
    {
        #region Statics
        public static ProductCatalogField ProductId
        {
            get
            {
                return new ProductCatalogField(1, 8, InputType.Number);
            }
        }

        public static ProductCatalogField ProductDescription { 
            get 
            {
                return new ProductCatalogField(10, 68, InputType.String);
            } 
        }

        public static ProductCatalogField RegularSinglarPrice
        { 
            get
            {
                return new ProductCatalogField(70, 77, InputType.Currency);
            }
        }

        public static ProductCatalogField PromotionalSinglarPrice
        {
            get
            {
                return new ProductCatalogField(79, 86, InputType.Currency);
            }
        }

        public static ProductCatalogField RegularSplitPrice
        {
            get
            {
                return new ProductCatalogField(88, 95, InputType.Currency);
            }
        }

        public static ProductCatalogField PromotionalSplitPrice
        {
            get
            {
                return new ProductCatalogField(97, 104, InputType.Currency);
            }
        }

        public static ProductCatalogField RegularForX
        {
            get
            {
                return new ProductCatalogField(106, 113, InputType.Number);
            }
        }

        public static ProductCatalogField PromotionalForX
        {
            get
            {
                return new ProductCatalogField(115, 122, InputType.Number);
            }
        }

        public static ProductCatalogField Flags
        {
            get
            {
                return new ProductCatalogField(124, 132, InputType.Flags);
            }
        }

        public static ProductCatalogField ProductSize
        {
            get
            {
                return new ProductCatalogField(134, 142, InputType.String);
            }
        }
        #endregion

        #region Constructor
        public ProductCatalogField(int start, int end, InputType inputType)
        {
            Start = start;
            End = end;
            InputType = inputType;
        }
        #endregion

        #region Properties
        public int Start { get; set; }
        public int End { get; set; }
        public InputType InputType { get; set; }
        #endregion
    }
}
