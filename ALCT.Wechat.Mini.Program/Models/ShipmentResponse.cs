using System.Collections.Generic;

namespace ALCT.Wechat.Mini.Program.Models
{
    public class ShipmentResponse : BasicResponseModel
    {
        public IList<Shipment> Shipments{get; set;}
    }
}