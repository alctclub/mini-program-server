using ALCT.Wechat.Mini.Program.Models;
using ALCT.Wechat.Mini.Program.BusinessLogics;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ALCT.Wechat.Mini.Program.Controllers
{
    public class BasicController : Controller
    {
        protected BasicResponseModel response;
        protected IBasicBusinessLogic basicBusinessLogic;
        protected string GetSessionId() 
        {
            return HttpContext.Request.Headers["SessionId"];
        }

        protected bool CheckSessionId() 
        {
            var sessionId = GetSessionId();
            if(string.IsNullOrEmpty(sessionId)) 
            {
                response = new BasicResponseModel();
                response.Code = 401;
                response.Message = "请退出小程序后重新进入操作";
                return false;
            }
            else
            {
                var token = basicBusinessLogic.GetToken(sessionId);
                if(token == null)
                {
                    response = new BasicResponseModel();
                    response.Code = 401;
                    response.Message = "请退出小程序后重新进入操作";
                    return false;
                }
            }
            return true;
        }
    }
}