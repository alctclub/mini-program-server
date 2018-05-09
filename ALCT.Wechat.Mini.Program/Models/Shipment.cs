using System;
using System.Collections.Generic;

namespace ALCT.Wechat.Mini.Program.Models
{
    public class Shipment : BasicResponseModel
    {
        public string ShipmentCode { get; set; }

        public string EnterpriseName { get; set; }

        public string StartAddress { get; set; }

        public string EndAddress { get; set; }

        public double TotalWeight { get; set; }

        public double TotalVolume { get; set; }

        public string LicensePlateNumber { get; set; }

        public long ShipmentCharge { get; set; }

        public DateTime? ShipmentConfirmDate { get; set; }

        public bool EnableNFC{ get; set; }

        public int StatusCode { get; set; }
        
        public IList<Order> Orders {get; set;}
    }
}