using ALCT.Wechat.Mini.Program.Models;
using ALCT.Wechat.Mini.Program.Agents;
using ALCT.Wechat.Mini.Program.Databases;

using Senparc.Weixin;
using Senparc.Weixin.MP;
using Senparc.Weixin.WxOpen.Containers;
using Senparc.Weixin.WxOpen.Entities.Request;
using Senparc.Weixin.WxOpen.AdvancedAPIs.Sns;

using System;
using System.Threading;
using System.Linq;
using Newtonsoft.Json;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Hosting;

namespace ALCT.Wechat.Mini.Program.BusinessLogics
{
    public class AuthenticationBusinessLogic : BasicBusinessLogic, IAuthenticationBusinessLogic
    {
        private readonly IHostingEnvironment environment;
        public AuthenticationBusinessLogic(MPDbContext dbContext,
            IAuthenticationAgent authenticationAgent,
            IConfigurationService configurationService,
            IHostingEnvironment env,
            ILogger<AuthenticationBusinessLogic> logger) 
        {
            this.dbContext = dbContext;
            this.authenticationAgent = authenticationAgent;
            this.configurationService = configurationService;
            this.wxConfiguration = configurationService.GetWechatConfiguration();
            this.aLCTConfiguration = configurationService.GetALCTConfiguration();
            this.environment = env;
            this.logger = logger;
        }

        public LoginResponse Login(string weiXinCode)
        {
            var response = new LoginResponse();

            try
            {
                var sessionId = GetWechatSessionId(weiXinCode);
                var openId = GetOpenId(sessionId);

                if(string.IsNullOrEmpty(openId)) 
                {
                    response.Code = AuthenticationErrorCode.FailedGetWxOpenId;
                    response.Message = "登陆失败，请关闭小程序，重新进入";
                    return response;
                }

                var member = (from m in dbContext.Set<Member>() 
                                where m.OpenId == openId
                                select m).FirstOrDefault();
                if(member == null) 
                {
                    response.Code = AuthenticationErrorCode.NewAccountNeedBind;
                    response.Message = "新用户，请先绑定手机号";
                    return response;
                }

                var token = GetToken(member);
                
                if(token == null) 
                {
                    response.Code = AuthenticationErrorCode.LoginFailed;
                    response.Message = "登陆失败，请关闭小程序，重新进入";
                    return response;
                } 
                else 
                {
                    response.SessionId = sessionId;
                }
            }
            catch(Exception ex) 
            {
                logger.LogError(ex, "Login failed with exception");
            }

            return response;
        }

        public BasicResponseModel SendVerificationCode(string phoneNumber)
        {
            var response = new BasicResponseModel();
            try
            {
                var alctResponse = authenticationAgent.SendVerificationCode(phoneNumber, GetApiToken().AccessToken);
                if(alctResponse.Code == "0") 
                {
                    response.Code = 0;
                    response.Message = "验证码发送成功";
                }
                else if(alctResponse.Code == AuthenticationErrorCode.AccessTokenInvalid.ToString()) 
                {
                    response.Code = AuthenticationErrorCode.AccessTokenInvalid;
                    response.Message = alctResponse.Message;
                }
                else 
                {
                    response.Code = AuthenticationErrorCode.PhoneNumberInvalid;
                    response.Message = "手机号码不存在，请检查后重试";
                }
            }
            catch(Exception ex) 
            {
                logger.LogError(ex, "Send verification failed");
                response.Code = AuthenticationErrorCode.SystemUnhandledException;
                response.Message = "验证码发送失败";
            }

            return response;
        }

        public LoginResponse BindWechatAccount(BindWechatAccountRequest request)
        {
            var response = new LoginResponse();
            var sessionId = GetWechatSessionId(request.WeiXinCode);
            var openId = GetOpenId(sessionId);
            if(string.IsNullOrEmpty(openId)) 
            {
                response.Code = AuthenticationErrorCode.FailedGetWxOpenId;
                response.Message = "验证失败，请关闭小程序，重新进入";
                return response;
            }

            var alctResponse = authenticationAgent.VerifyVerificationCode(request.PhoneNumber, request.VerificationCode, GetApiToken().AccessToken);
            if(alctResponse.Code == "1") 
            {
                response.Code = AuthenticationErrorCode.PhoneNumberInvalid;
                response.Message = "手机号码不存在，请检查后重试";
                return response;
            }
            else if(alctResponse.Code == "2") 
            {
                response.Code = AuthenticationErrorCode.InvalidVerificationCode;
                response.Message = "验证码错误，请检查后重试";
                return response;
            }

            var member = new Member()
            {
                OpenId = openId,
                DriverIdentification = alctResponse.DriverIdentity,
                CreatedDate = DateTime.Now
            };
            
            using(var transaction = dbContext.Database.BeginTransaction()) 
            {
                dbContext.Add(member);
                dbContext.SaveChanges();
                transaction.Commit();
            }
            
            var token = GetToken(member);
            if(token == null) 
            {
                response.Code = AuthenticationErrorCode.LoginFailed;
                response.Message = "登陆失败，请关闭小程序，重新进入";
                return response;
            }

            response.Code = 0;
            response.SessionId = sessionId;
            return response;
        }

        private string GetWechatSessionId(string weiXinCode)
        {
            if(environment.EnvironmentName == "dev" || environment.EnvironmentName == "qa") 
            {
                var tag = SessionContainer.UpdateSession(null, "071TIDkB14xCof0eHekB1I4NkB1TIDk5", "071TIDkB14xCof0eHekB1I4NkB1TIDk5");
                return tag.Key;
            }
            
            var jsonResult = SnsApi.JsCode2Json(wxConfiguration.AppId, wxConfiguration.AppSecret, weiXinCode);
            if (jsonResult.errcode == ReturnCode.请求成功)
            {
                SessionContainer.UpdateSession(null, jsonResult.openid, jsonResult.session_key);
                return jsonResult.openid;
            }
            else
            {
                logger.LogError(JsonConvert.SerializeObject(jsonResult));
            }
            return string.Empty;
        }
    }
}