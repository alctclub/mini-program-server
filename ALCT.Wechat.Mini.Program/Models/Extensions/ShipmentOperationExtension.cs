namespace ALCT.Wechat.Mini.Program.Models
{
    public static class ShipmentOperationExtension
    {
        public static ALCTShipmentOperation ToALCTOperation(this ShipmentEventRequest request) 
        {
            if(request == null)
            {
                return null;
            }

            return new ALCTShipmentOperation() 
            {
                ShipmentCode = request.ShipmentCode,
                BaiduLatitude = (decimal)(request.LatitudeValue ?? 0),
                BaiduLongitude = (decimal)(request.LongitudeValue ?? 0),
                Location = request.Location,
                operationTime = request.TraceDate
            };
        }
    }
}