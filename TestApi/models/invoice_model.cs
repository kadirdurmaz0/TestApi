using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApi.models
{
    public class InvoiceModel
    {
        public string orderCode { get; set; }
        public string orderDate { get; set; }
        public string systemTypeCodes { get; set; }
        public string invoiceDate { get; set; }
        public string invoiceExplanation { get; set; }
        public string eInvoiceId { get; set; }
        public bool isDocumentNoAuto { get; set; }
        public string ettn { get; set; }
        public string receiverTag { get; set; }
        public string billingName { get; set; }
        public string billingAddress { get; set; }
        public string billingTown { get; set; }
        public string billingCity { get; set; }
        public string billingMobilePhone { get; set; }
        public string billingPhone { get; set; }
        public string billingPhone2 { get; set; }
        public string taxOffice { get; set; }
        public string taxNo { get; set; }
        public string email { get; set; }
        public string shipCompany { get; set; }
        public string cargoCampaignCode { get; set; }
        public string shippingName { get; set; }
        public string shippingAddress { get; set; }
        public string shippingTown { get; set; }
        public string shippingCity { get; set; }
        public string shippingCountry { get; set; }
        public string shippingZipCode { get; set; }
        public string shippingPhone { get; set; }
        public int deliveryFeeType { get; set; }
        public string paymentType { get; set; }
        public string currency { get; set; }
        public int currencyRate { get; set; }
        public int totalPaidTaxExcluding { get; set; }
        public int totalPaidTaxIncluding { get; set; }
        public int productsTotalTaxExcluding { get; set; }
        public int productsTotalTaxIncluding { get; set; }
        public int shippingChargeTotalTaxExcluding { get; set; }
        public int shippingChargeTotalTaxIncluding { get; set; }
        public int installmentChargeTotalTaxExcluding { get; set; }
        public int installmentChargeTotalTaxIncluding { get; set; }
        public int bankTransferDiscountTotalTaxExcluding { get; set; }
        public int bankTransferDiscountTotalTaxIncluding { get; set; }
        public int payingAtTheDoorChargeTotalTaxExcluding { get; set; }
        public int payingAtTheDoorChargeTotalTaxIncluding { get; set; }
        public int discountTotalTaxExcluding { get; set; }
        public int discountTotalTaxIncluding { get; set; }
        public List<OrderDetailModel> orderDetails { get; set; }
    }
}
