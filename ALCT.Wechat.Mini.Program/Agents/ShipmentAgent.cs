using ALCT.Wechat.Mini.Program.Models;
using ALCT.Wechat.Mini.Program.Utils;

using System;
using System.Net.Http;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace ALCT.Wechat.Mini.Program.Agents
{
    public class ShipmentAgent : BasicAgent, IShipmentAgent
    {
        public ShipmentAgent() 
        {
            this.aLCTConfiguration = new ALCTConfiguration();
        }

        public IList<Shipment> GetExecutingShipments(string token) 
        {
            var result = Request(aLCTConfiguration.OpenApiHost + aLCTConfiguration.GetExecutingShipmentUrl, HttpMethod.Get, null, 
                new Dictionary<string, string>
                {
                    [HttpHeaderKeys.AccessToken] = token
                });
            
            return JsonConvert.DeserializeObject<IList<Shipment>>(result);
        }

        public Shipment GetShipmentDetail(string token, string shipmentCode) 
        {
            var result = Request(string.Format(aLCTConfiguration.OpenApiHost + aLCTConfiguration.GetShipmentDetailUrl, shipmentCode), HttpMethod.Get, null, 
                new Dictionary<string, string>
                {
                    [HttpHeaderKeys.AccessToken] = token
                });
            
            return JsonConvert.DeserializeObject<Shipment>(result);
        }

        public ALCTBasicResponse HandleShipmentEvent(string token, string operate, ALCTShipmentOperation operation) 
        {
            var result = Request(string.Format(aLCTConfiguration.OpenApiHost + aLCTConfiguration.ShipmentEventUrl, operate), HttpMethod.Post, operation, 
                new Dictionary<string, string>
                {
                    [HttpHeaderKeys.AccessToken] = token
                });
            
            return JsonConvert.DeserializeObject<ALCTBasicResponse>(result);
        }
    }
}