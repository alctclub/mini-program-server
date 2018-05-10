using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;

using ALCT.Wechat.Mini.Program.Agents;
using ALCT.Wechat.Mini.Program.Models;
using ALCT.Wechat.Mini.Program.Databases;

namespace ALCT.Wechat.Mini.Program.BusinessLogics
{
    public class InvoiceBusinessLogic : BasicBusinessLogic, IInvoiceBusinessLogic
    {
        private readonly IDriverInvoiceAgent driverInvoiceAgent;
        public InvoiceBusinessLogic(MPDbContext dbContext,
            IAuthenticationAgent authenticationAgent,
            IConfigurationService configurationService,
            IDriverInvoiceAgent driverInvoiceAgent,
            ILogger<ShipmentBusinessLogic> logger)
        {
            this.dbContext = dbContext;
            this.authenticationAgent = authenticationAgent;
            this.configurationService = configurationService;
            this.driverInvoiceAgent = driverInvoiceAgent;
            this.aLCTConfiguration = configurationService.GetALCTConfiguration();
            this.logger = logger;
        }

        public InvoiceResponse GetInvoices(string sessionId)
        {
            try
            {
                var response = driverInvoiceAgent.GetDriverInvoices(GetToken(sessionId).AccessToken);
                return response;
            }
            catch(Exception ex)
            {
                logger.LogError(ex, "Get invoice failed");
            }

            return new InvoiceResponse()
            {
                Code = InvoiceErrorCode.SystemUnhandledException,
                Message = "发票获取失败，请重试"
            };
        }

        public BasicResponseModel ConfirmInvoice(string sessionId, string invoiceCode)
        {
            try
            {
                var alctResponse = driverInvoiceAgent.ConfirmDriverInvoice(GetToken(sessionId).AccessToken, new ALCTConfirmInvoiceRequest() 
                {
                    DriverInvoiceCode = invoiceCode
                });
                
                if(alctResponse.Code != "0") 
                {
                    return new BasicResponseModel() 
                    {
                        Code = InvoiceErrorCode.ConfrimInvoiceFailed,
                        Message = alctResponse.Message
                    };
                }

                return new BasicResponseModel();
            }
            catch(Exception ex)
            {
                logger.LogError(ex, "Get invoice failed");
            }
            
            return new InvoiceResponse()
            {
                Code = InvoiceErrorCode.SystemUnhandledException,
                Message = "发票确认失败，请重试"
            };
        }
    }
}