using ALCT.Wechat.Mini.Program.BusinessLogics;
using ALCT.Wechat.Mini.Program.Models;
using ALCT.Wechat.Mini.Program.Utils;

using System;
using System.Net.Http;
using Newtonsoft.Json;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;

namespace ALCT.Wechat.Mini.Program.Agents
{
    public class ImageAgent : BasicAgent, IImageAgent
    {
        public ImageAgent(IConfigurationService configurationService, 
            ILogger<ImageAgent> logger) 
        {
            this.logger = logger;
            this.aLCTConfiguration = configurationService.GetALCTConfiguration();
        }

        public ALCTBasicResponse UploadImage(string token, ALCTUploadFileModel request) 
        {
            var result = Request(aLCTConfiguration.OpenApiHost + aLCTConfiguration.UploadShipmentImageUrl, HttpMethod.Post, request, 
                new Dictionary<string, string>
                {
                    [HttpHeaderKeys.AccessToken] = token
                });
            
            if(!string.IsNullOrEmpty(result)) 
            {
                return JsonConvert.DeserializeObject<ALCTBasicResponse>(result);
            }
            else 
            {
                return new ALCTBasicResponse() 
                {
                    Code = "0"
                };
            }
        }

        public ALCTBasicResponse DeleteImage(string token, string shipmentCode, string fileName, string imageType)
        {
            var result = Request(string.Format(aLCTConfiguration.OpenApiHost + aLCTConfiguration.DeleteShipmentImageUrl, shipmentCode, System.Web.HttpUtility.UrlEncode(fileName), imageType)
                , HttpMethod.Delete, null, 
                new Dictionary<string, string>
                {
                    [HttpHeaderKeys.AccessToken] = token
                });

            
            if(!string.IsNullOrEmpty(result)) 
            {
                return JsonConvert.DeserializeObject<ALCTBasicResponse>(result);
            }
            else 
            {
                return new ALCTBasicResponse() 
                {
                    Code = "0"
                };
            }
        }

        public IList<ALCTGetFileDataModel> GetImages(string token, string shipmentCode, string imageType) 
        {
            var result = Request(string.Format(aLCTConfiguration.OpenApiHost + aLCTConfiguration.GetShipmentImagesUrl, shipmentCode, imageType)
                , HttpMethod.Get, null, 
                new Dictionary<string, string>
                {
                    [HttpHeaderKeys.AccessToken] = token
                });
            
            return JsonConvert.DeserializeObject<IList<ALCTGetFileDataModel>>(result);
        }
    }
}