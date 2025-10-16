using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApi.models
{
    public class OrderDetailModel
    {
        public string productCode { get; set; }
        public string barcode { get; set; }
        public string productBrand { get; set; }
        public string productName { get; set; }
        public string productNote { get; set; }
        public string productQuantityType { get; set; }
        public int productQuantity { get; set; }
        public int vatRate { get; set; }
        public int productUnitPriceTaxExcluding { get; set; }
        public int productUnitPriceTaxIncluding { get; set; }
        public int discountIsPercentUnit { get; set; }
        public int discountRateUnit { get; set; }
        public int discountUnitTaxExcluding { get; set; }
        public int discountUnitTaxIncluding { get; set; }
    }
}
