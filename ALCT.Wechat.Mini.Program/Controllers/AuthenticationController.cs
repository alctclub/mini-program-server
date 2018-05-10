using ALCT.Wechat.Mini.Program.Utils;
using ALCT.Wechat.Mini.Program.Models;
using ALCT.Wechat.Mini.Program.BusinessLogics;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;


namespace ALCT.Wechat.Mini.Program.Controllers
{
    [Route("api/v1/miniprogram/auth")]
    public class AuthenticationController : BasicController
    {
        private readonly IAuthenticationBusinessLogic authenticationBusinessLogic;
        public AuthenticationController(IAuthenticationBusinessLogic authenticationBusinessLogic) 
        {
            this.authenticationBusinessLogic = authenticationBusinessLogic;
        }

        [HttpGet]
        [Route("verification-code/{phoneNumber}")]
        public IActionResult SendVerificationCode(string phoneNumber) 
        {
            return Ok(authenticationBusinessLogic.SendVerificationCode(phoneNumber));
        }

        [HttpPost]
        [Route("bind")]
        public IActionResult BindWechatAccount([FromBody]BindWechatAccountRequest request) 
        {
            return Ok(authenticationBusinessLogic.BindWechatAccount(request));
        }

        [HttpPost]
        [Route("login")]
        public IActionResult Login([FromBody]LoginRequest request) 
        {
            return Ok(authenticationBusinessLogic.Login(request.WeiXinCode));
        }
    }
}