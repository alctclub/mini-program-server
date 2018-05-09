using System;

namespace ALCT.Wechat.Mini.Program.Models
{
    public class Order
    {
        public string ShipmentCode { get; set; }

        public string OrderCode { get; set; }

        public string PickUpAddress { get; set; }

        public string ArrivalAddress { get; set; }

        public string Consignor { get; set; }

        public string ConsignorPhoneNumber { get; set; }

        public DateTime RequirePickupStartDate { get; set; }

        public DateTime RequirePickupEndDate { get; set; }

        public DateTime RequireArrivalStartDate { get; set; }

        public DateTime RequireArrivalEndDate { get; set; }

        public string Consignee { get; set; }

        public string ConsigneePhoneNumber { get; set; }

        public bool IsSignException { get; set; }

        public int StatusCode { get; set; }

        public bool CheckSignCode { get; set; }

        public int NextStatusCode { get; set; }

        public string AllowAction { get; set; }
    }
}