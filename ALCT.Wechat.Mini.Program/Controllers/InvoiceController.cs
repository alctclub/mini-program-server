using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

using ALCT.Wechat.Mini.Program.Models;
using ALCT.Wechat.Mini.Program.BusinessLogics;

namespace ALCT.Wechat.Mini.Program.Controllers
{
    [Route("api/v1/miniprogram/invoices")]
    public class InvoiceController : BasicController
    {
        private readonly IInvoiceBusinessLogic invoiceBusinessLogic;
        public InvoiceController(IInvoiceBusinessLogic invoiceBusinessLogic) 
        {
            this.invoiceBusinessLogic = invoiceBusinessLogic;
            this.basicBusinessLogic = (BasicBusinessLogic)invoiceBusinessLogic;
        }

        [HttpGet]
        [Route("")]
        public IActionResult GetInvoices()
        {
            if(!CheckSessionId()) 
            {
                return Ok(response);
            }
            return Ok(invoiceBusinessLogic.GetInvoices(GetSessionId()));
        }

        [HttpPut]
        [Route("confirm")]
        public IActionResult ConfirmInvoice(ConfirmInvoiceRequest request) 
        {
            if(!CheckSessionId()) 
            {
                return Ok(response);
            }
            
            return Ok(invoiceBusinessLogic.ConfirmInvoice(GetSessionId(), request.DriverInvoiceCode));
        }
    }
}