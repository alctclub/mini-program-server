using System.Collections.Generic;
using ALCT.Wechat.Mini.Program.Models;

namespace ALCT.Wechat.Mini.Program.BusinessLogics
{
    public interface IShipmentBusinessLogic
    {
         ShipmentResponse GetShipments(string sessionId);
         Shipment GetShipmentDetail(string sessionId, string shipmentCode);
         IList<GoodsModel> GetOrderGoods(string sessionId, string shipmentCode, string orderCode);
         BasicResponseModel HandleEvent(string sessionId, string operate, ShipmentEventRequest request);
    }
}