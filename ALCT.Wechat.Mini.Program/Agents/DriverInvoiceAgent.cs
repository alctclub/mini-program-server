using ALCT.Wechat.Mini.Program.Models;
using ALCT.Wechat.Mini.Program.Utils;

using System;
using System.Net.Http;
using Newtonsoft.Json;
using System.Collections.Generic;


namespace ALCT.Wechat.Mini.Program.Agents
{
    public class DriverInvoiceAgent : BasicAgent, IDriverInvoiceAgent
    {
        public DriverInvoiceAgent() 
        {
            this.aLCTConfiguration = new ALCTConfiguration();
        }

        public InvoiceResponse GetDriverInvoices(string token)
        {
            var result = Request(aLCTConfiguration.OpenApiHost + aLCTConfiguration.GetDriverInvoiceUrl, HttpMethod.Get, null, 
                new Dictionary<string, string>
                {
                    [HttpHeaderKeys.AccessToken] = token
                });
            
            return JsonConvert.DeserializeObject<InvoiceResponse>(result);
        }
        
        public ALCTBasicResponse ConfirmDriverInvoice(string token, ALCTConfirmInvoiceRequest request)
        {
            var result = Request(aLCTConfiguration.OpenApiHost + aLCTConfiguration.GetDriverInvoiceUrl, HttpMethod.Put, request, 
                new Dictionary<string, string>
                {
                    [HttpHeaderKeys.AccessToken] = token
                });
            
            return JsonConvert.DeserializeObject<ALCTBasicResponse>(result);
        }
    }
}