using ALCT.Wechat.Mini.Program.Models;

namespace ALCT.Wechat.Mini.Program.Agents
{
    public interface IDriverInvoiceAgent
    {
        InvoiceResponse GetDriverInvoices(string token);
        ALCTBasicResponse ConfirmDriverInvoice(string token, ALCTConfirmInvoiceRequest request);
    }
} 