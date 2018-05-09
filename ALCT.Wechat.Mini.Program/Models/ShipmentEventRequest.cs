using System;

namespace ALCT.Wechat.Mini.Program.Models
{
    public class ShipmentEventRequest
    {
        public string ShipmentCode {get; set;}
        public string OrderCode {get; set;}
        public double? LatitudeValue {get; set;}
        public double? LongitudeValue {get; set;}
        public string Location {get; set;}
        public DateTime TraceDate {get; set;}
        public string FormId {get; set;}
    }
}