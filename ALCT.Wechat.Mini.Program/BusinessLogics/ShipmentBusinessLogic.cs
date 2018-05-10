using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;

using ALCT.Wechat.Mini.Program.Agents;
using ALCT.Wechat.Mini.Program.Models;
using ALCT.Wechat.Mini.Program.Databases;

namespace ALCT.Wechat.Mini.Program.BusinessLogics
{
    public class ShipmentBusinessLogic : BasicBusinessLogic, IShipmentBusinessLogic
    {
        private readonly IShipmentAgent shipmentAgent;
        private string[] AllowOperates = new string[] {"pickup", "unload", "sign", "pod"};
        public ShipmentBusinessLogic(MPDbContext dbContext,
            IAuthenticationAgent authenticationAgent,
            IConfigurationService configurationService,
            IShipmentAgent shipmentAgent,
            ILogger<ShipmentBusinessLogic> logger)
        {
            this.dbContext = dbContext;
            this.authenticationAgent = authenticationAgent;
            this.configurationService = configurationService;
            this.shipmentAgent = shipmentAgent;
            this.aLCTConfiguration = configurationService.GetALCTConfiguration();
            this.logger = logger;
        }

        public ShipmentResponse GetShipments(string sessionId) 
        {
            var response = new ShipmentResponse();
            try
            {
                response.Shipments = shipmentAgent.GetExecutingShipments(GetToken(sessionId).AccessToken);
                response.Code = 0;
                response.Message = "";
            }
            catch(Exception ex)
            {
                logger.LogError(ex, "Get shipments failed");
                response.Code = ShipmentErrorCode.SystemUnhandledException;
                response.Message = "运单获取失败，请重新进入小程序后重试";
            }
            return response;
        }

        public Shipment GetShipmentDetail(string sessionId, string shipmentCode) 
        {
            try
            {
                return shipmentAgent.GetShipmentDetail(GetToken(sessionId).AccessToken, shipmentCode);
            }
            catch(Exception ex) 
            {                
                logger.LogError(ex, "Get shipment detail failed");
                return new Shipment() 
                {
                    Code = ShipmentErrorCode.SystemUnhandledException,
                    Message = "运单获取失败，请返回后重试"
                };
            }
        }

        public IList<GoodsModel> GetOrderGoods(string sessionId, string shipmentCode, string orderCode)
        {
            return null;
        }

        public BasicResponseModel HandleEvent(string sessionId, string operate, ShipmentEventRequest request)
        {
            var response = new BasicResponseModel();
            if(!AllowOperates.Contains(operate)) 
            {
                response.Code = ShipmentErrorCode.InvalidShipmentOperate;
                response.Message = "非法操作";
                return response;
            }

            var alctResponse = shipmentAgent.HandleShipmentEvent(GetToken(sessionId).AccessToken, operate, request.ToALCTOperation());
            if(alctResponse == null) 
            {
                response.Code = ShipmentErrorCode.ShipmentOperationFailed;
                response.Message = "操作失败，请重试";
            }
            else if(alctResponse.Code == "1")
            {
                response.Code = ShipmentErrorCode.ShipmentOperationFailed;
                response.Message = "操作失败，请重试";
            }
            else if(alctResponse.Code == "2")
            {
                response.Code = ShipmentErrorCode.ShipmentOperationFailed;
                response.Message = "操作失败，请刷新后重试";
            }
            else if(alctResponse.Code == "0")
            {
                response.Code = 0;
                response.Message = "操作成功";
            }
            return response;
        }
    }
}