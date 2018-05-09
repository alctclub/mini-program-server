namespace ALCT.Wechat.Mini.Program.Models
{
    public class InvoiceModel
    {
        public string driverInvoiceCode {get; set;}
        public string invoiceReceiverName {get; set;}
        public decimal taxRate {get; set;}
        public decimal taxAmount {get; set;}
        public decimal totalAmount {get; set;}
        public decimal totalAmountIncludeTax {get; set;}
    }
}