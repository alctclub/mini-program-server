using ALCT.Wechat.Mini.Program.BusinessLogics;
using ALCT.Wechat.Mini.Program.Models;
using ALCT.Wechat.Mini.Program.Utils;

using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Collections.Generic;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;

namespace ALCT.Wechat.Mini.Program.Agents
{
    public class AuthenticationAgent : BasicAgent, IAuthenticationAgent
    {
        public AuthenticationAgent(IConfigurationService configurationService, 
            ILogger<AuthenticationAgent> logger)
        {
            this.aLCTConfiguration = configurationService.GetALCTConfiguration();
            this.logger = logger;
        }

        public ALCTLoginResponse DoDriverLogin(ALCTDriverLoginRequest request) 
        {
            ALCTLoginResponse response = null;
            var responseString = Request(aLCTConfiguration.OpenApiHost + aLCTConfiguration.DriverLoginUrl, HttpMethod.Post, request);
            if(!string.IsNullOrEmpty(responseString)) 
            {
                response = JsonConvert.DeserializeObject<ALCTLoginResponse>(responseString);
                if(response.Code != "0") 
                {
                    response = null;
                    logger.LogError("ALCT Login Failed:" + responseString);
                }
            }
            return response;
        } 
        
        public ALCTLoginResponse DoApiLogin(ALCTApiLoginRequest request) 
        {
            ALCTLoginResponse response = null;
            var responseString = Request(aLCTConfiguration.OpenApiHost + aLCTConfiguration.ApiLoginUrl, HttpMethod.Post, request);
            if(!string.IsNullOrEmpty(responseString)) 
            {
                response = JsonConvert.DeserializeObject<ALCTLoginResponse>(responseString);
                if(response.Code != "0") 
                {
                    response = null;
                    logger.LogError("ALCT Login Failed:" + responseString);
                }
            }
            return response;
        }

        public ALCTBasicResponse SendVerificationCode(string phoneNumber, string accessToken) 
        {
            if(string.IsNullOrEmpty(accessToken.Trim())) 
            {
                logger.LogError("Send verification failed caused empty token");
                return new ALCTBasicResponse()
                {
                    Code = AuthenticationErrorCode.AccessTokenInvalid.ToString(),
                    Message = "短信发送失败"
                };
            }

            var response = Request(string.Format(aLCTConfiguration.OpenApiHost + aLCTConfiguration.SendVerificationCodeUrl, phoneNumber), HttpMethod.Get, null,
                new Dictionary<string, string>()
                {
                    [HttpHeaderKeys.AccessToken] = accessToken
                });
            
            return JsonConvert.DeserializeObject<ALCTBasicResponse>(response);
        }

        public ALCTVerifyVerificationCodeResponse VerifyVerificationCode(string phoneNumber, string code, string accessToken) 
        {
            var response = Request(string.Format(aLCTConfiguration.OpenApiHost + aLCTConfiguration.VerifyVerificationCodeUrl, phoneNumber, code), HttpMethod.Get, null,
                new Dictionary<string, string>()
                {
                    [HttpHeaderKeys.AccessToken] = accessToken
                });
            
            return JsonConvert.DeserializeObject<ALCTVerifyVerificationCodeResponse>(response);
        }
    }
}