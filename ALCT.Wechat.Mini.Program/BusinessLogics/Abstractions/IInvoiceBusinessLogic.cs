using ALCT.Wechat.Mini.Program.Models;

namespace ALCT.Wechat.Mini.Program.BusinessLogics
{
    public interface IInvoiceBusinessLogic
    {
         InvoiceResponse GetInvoices(string sessionId);
         BasicResponseModel ConfirmInvoice(string sessionId, string invoiceCode);
    }
}