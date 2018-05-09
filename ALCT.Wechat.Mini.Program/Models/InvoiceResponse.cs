using System.Collections.Generic;

namespace ALCT.Wechat.Mini.Program.Models
{
    public class InvoiceResponse : BasicResponseModel
    {
        public IList<InvoiceModel> DriverInvoices {get; set;}
    }
}