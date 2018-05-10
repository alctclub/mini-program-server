using ALCT.Wechat.Mini.Program.BusinessLogics;
using ALCT.Wechat.Mini.Program.Models;
using ALCT.Wechat.Mini.Program.Utils;

using System;
using System.Net.Http;
using Newtonsoft.Json;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;


namespace ALCT.Wechat.Mini.Program.Agents
{
    public class DriverInvoiceAgent : BasicAgent, IDriverInvoiceAgent
    {
        public DriverInvoiceAgent(IConfigurationService configurationService, 
            ILogger<DriverInvoiceAgent> logger) 
        {
            this.logger = logger;
            this.aLCTConfiguration = configurationService.GetALCTConfiguration();
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
            var result = Request(aLCTConfiguration.OpenApiHost + aLCTConfiguration.ConfirmDriverInvoiceUrl, HttpMethod.Put, request, 
                new Dictionary<string, string>
                {
                    [HttpHeaderKeys.AccessToken] = token
                });
            
            if(!string.IsNullOrEmpty(result)) 
            {
                return JsonConvert.DeserializeObject<ALCTBasicResponse>(result);
            }
            else 
            {
                return new ALCTBasicResponse()
                {
                    Code = "0"
                };
            }
        }
    }
}