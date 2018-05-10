using ALCT.Wechat.Mini.Program.Models;
using ALCT.Wechat.Mini.Program.Databases;
using ALCT.Wechat.Mini.Program.Agents;

using Senparc.Weixin.WxOpen.Containers;

using System;
using System.Linq;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore.Storage;

namespace ALCT.Wechat.Mini.Program.BusinessLogics
{
    public class BasicBusinessLogic : IBasicBusinessLogic
    {        
        protected MPDbContext dbContext;
        protected IAuthenticationAgent authenticationAgent;
        protected ALCTConfiguration aLCTConfiguration;
        protected WechatConfiguration wxConfiguration;
        protected ILogger logger;
        protected IConfigurationService configurationService;

        protected Token GetApiToken() 
        {
            var token = (from t in dbContext.Set<Token>()
                            where t.OpenId == aLCTConfiguration.EnterpriseCode && t.ExpiryDate < DateTime.Now
                            orderby t.Id descending
                            select t).FirstOrDefault();

            if(token == null) 
            {
                var response = authenticationAgent.DoApiLogin(new ALCTApiLoginRequest() 
                {
                    EnterpriseCode = aLCTConfiguration.EnterpriseCode,
                    EnterpriseIdentity = aLCTConfiguration.AppIdentity,
                    EnterpriseKey = aLCTConfiguration.AppKey
                });

                if(response != null) 
                {
                    token = response.ToToken(aLCTConfiguration.EnterpriseCode);
                    using(var transaction = dbContext.Database.BeginTransaction()) 
                    {
                        var expiredTokens = (from t in dbContext.Set<Token>() where t.OpenId == aLCTConfiguration.EnterpriseCode select t).ToList();
                        dbContext.RemoveRange(expiredTokens);
                        dbContext.Add(token);
                        transaction.Commit();
                    }
                }
            }
            return token;
        }
        
        public Token GetToken(string sessionId)
        {
            var openId = GetOpenId(sessionId);
            if(string.IsNullOrEmpty(openId))
            {
                return null;
            }
            
            var member = (from m in dbContext.Set<Member>()
                            where m.OpenId == openId
                            select m).FirstOrDefault();
            
            if(member == null) 
            {
                return null;
            }

            return GetToken(member);
        }

        protected Token GetToken(Member member) 
        {
            var token = (from t in dbContext.Set<Token>()
                            where t.OpenId == member.OpenId && t.ExpiryDate > DateTime.Now
                            orderby t.Id descending
                            select t).FirstOrDefault();

            if(token == null) 
            {
                var response = authenticationAgent.DoDriverLogin(new ALCTDriverLoginRequest() 
                {
                    EnterpriseCode = aLCTConfiguration.EnterpriseCode,
                    AppIdentity = aLCTConfiguration.AppIdentity,
                    AppKey = aLCTConfiguration.AppKey,
                    DriverIdentity = member.DriverIdentification
                });

                if(response != null) 
                {
                    token = response.ToToken(member.OpenId);
                    using(var transaction = dbContext.Database.BeginTransaction()) 
                    {
                        var expiredTokens = (from t in dbContext.Set<Token>() where t.OpenId == member.OpenId select t).ToList();
                        dbContext.RemoveRange(expiredTokens);
                        dbContext.Add(token);
                        dbContext.SaveChanges();
                        transaction.Commit();
                    }
                }
            }
            return token;
        }

        protected string GetOpenId(string sessionId)
        {
            var tag = SessionContainer.GetSession(sessionId);
            return tag == null ? null : tag.OpenId;
        }
    }
}