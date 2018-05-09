using ALCT.Wechat.Mini.Program.Utils;
using ALCT.Wechat.Mini.Program.Models;

using System;
using System.IO;
using System.Text;
using System.Linq;
using System.Net.Http;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;

namespace ALCT.Wechat.Mini.Program.Agents
{
    public class BasicAgent
    {
        protected ILogger logger;
        protected ALCTConfiguration aLCTConfiguration;

        public string Request(string url, 
            HttpMethod method, 
            object body,
            IDictionary<string, string> header = null, 
            string AccessTokenType = "Bearer", 
            TimeSpan? timeout = null) 
        {
            HttpClient client = GenerateHttpClient(url);
            client.Timeout = timeout ?? new TimeSpan(0,0,30);
            HttpRequestMessage message = new HttpRequestMessage(method, url);
            var contentType = "application/json";

            if (null != header)
            {
                foreach (var key in header.Keys)
                {

                    if (key == HttpHeaderKeys.AccessToken)
                    {
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(AccessTokenType, header[key]);
                    }
                    else if(key == HttpHeaderKeys.ContentType) 
                    {                        
                        message.Content.Headers.ContentType = MediaTypeHeaderValue.Parse(header[key]);
                        contentType = header[key];
                    }
                    else 
                    {
                        message.Headers.Add(key, header[key]);
                    }
                }
            }

            if (body != null)
            {
                if (body is Stream)
                {
                    message.Content = new StreamContent(body as Stream);
                }
                else 
                {
                    var stringMessage = JsonConvert.SerializeObject(body);
                    message.Content = new StringContent(stringMessage, Encoding.UTF8, contentType);
                }
            } 

            var response = client.SendAsync(message).Result;
            if((int)response.StatusCode < 400 ) 
            {
                return response.Content.ReadAsStringAsync().Result;
            }
            else 
            {
                try
                {
                    logger.LogError("call api failed with:" + response.Content.ReadAsStringAsync().Result);
                } 
                finally 
                {

                }
            }

            return string.Empty;
        }

        private static HttpClient GenerateHttpClient(string url)
        {
            HttpClientHandler handler;

            if (url.StartsWith("https://", StringComparison.CurrentCultureIgnoreCase))
            {
                handler = new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = (message1, certificate2, chain, errors) =>
                    {
                        return true;
                    }
                };
            }
            else
            {
                    handler = new HttpClientHandler();
            }

            return new HttpClient(handler);
        }
    }
}