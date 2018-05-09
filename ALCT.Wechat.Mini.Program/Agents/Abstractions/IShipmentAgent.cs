using System.Collections.Generic;
using ALCT.Wechat.Mini.Program.Models;

namespace ALCT.Wechat.Mini.Program.Agents
{
    public interface IShipmentAgent
    {
        IList<Shipment> GetExecutingShipments(string token);
        Shipment GetShipmentDetail(string token, string shipmentCode);
        ALCTBasicResponse HandleShipmentEvent(string token, string operate, ALCTShipmentOperation operation);
    }
}