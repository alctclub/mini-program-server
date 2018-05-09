using System;

namespace ALCT.Wechat.Mini.Program.Models
{
    public class ALCTShipmentOperation
    {
        public string ShipmentCode {get; set;}
        public decimal? BaiduLongitude {get;set;}
        public decimal? BaiduLatitude {get;set;}
        public string Location {get; set;}
        public DateTime operationTime {get;set;} = System.DateTime.Now;
    }
}