using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using ALCT.Wechat.Mini.Program.BusinessLogics;
using ALCT.Wechat.Mini.Program.Models;

namespace ALCT.Wechat.Mini.Program.Controllers
{
    [Route("api/v1/miniprogram/shipments")]
    public class ShipmentController : BasicController
    {
        private readonly IShipmentBusinessLogic shipmentBusinessLogic;
        public ShipmentController(IShipmentBusinessLogic shipmentBusinessLogic) 
        {
            this.shipmentBusinessLogic = shipmentBusinessLogic;
            this.basicBusinessLogic = (IBasicBusinessLogic)shipmentBusinessLogic;
        }

        [HttpGet]
        [Route("")]
        public IActionResult GetShipments() 
        {
            if(!CheckSessionId()) 
            {
                return Ok(response);
            }

            return Ok(shipmentBusinessLogic.GetShipments(GetSessionId()));
        }

        [HttpGet]
        [Route("detail/{shipmentCode}")]
        public IActionResult GetShipmentDetail(string shipmentCode) 
        {
            if(!CheckSessionId()) 
            {
                return Ok(response);
            }

            return Ok(shipmentBusinessLogic.GetShipmentDetail(GetSessionId(), shipmentCode));
        }
        
        [HttpPost]
        [Route("events/{operate}")]
        public IActionResult ShipmentEvent([FromBody]ShipmentEventRequest request, string operate) 
        {
            if(!CheckSessionId()) 
            {
                return Ok(response);
            }

            return Ok(shipmentBusinessLogic.HandleEvent(GetSessionId(), operate, request));
        }
        
        [HttpGet]
        [Route("goods")]
        public IActionResult GetOrderGoods(string shipmentCode, string orderCode) 
        {
            if(!CheckSessionId()) 
            {
                return Ok(response);
            }

            return Ok(shipmentBusinessLogic.GetOrderGoods(GetSessionId(), shipmentCode, orderCode));
        }
    }
}
