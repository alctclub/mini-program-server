using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Senparc.Weixin.WxOpen.Entities.Request;
using Senparc.Weixin.MP;

namespace ALCT.Wechat.Mini.Program.Controllers
{
    [Route("api/v1/wx")]
    public class WxOpenController : BasicController
    {
        private string Token;

        public WxOpenController() 
        {

        }

        [HttpGet]
        [Route("Index")]
        public IActionResult Get(PostModel postModel, string echostr)
        {
            if (CheckSignature.Check(postModel.Signature, postModel.Timestamp, postModel.Nonce, Token))
            {
                return Ok(echostr); //返回随机字符串则表示验证通过
            }
            else
            {
                return Ok("failed:" + postModel.Signature + "," + CheckSignature.GetSignature(postModel.Timestamp, postModel.Nonce, Token) + "。" +
                    "如果你在浏览器中看到这句话，说明此地址可以被作为微信小程序后台的Url，请注意保持Token一致。");
            }
        }
    }
}