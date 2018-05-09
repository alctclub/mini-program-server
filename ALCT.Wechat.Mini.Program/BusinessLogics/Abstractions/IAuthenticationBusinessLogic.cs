using ALCT.Wechat.Mini.Program.Models;

namespace ALCT.Wechat.Mini.Program.BusinessLogics
{
    public interface IAuthenticationBusinessLogic
    {
        LoginResponse Login(string weiXinCode);
        BasicResponseModel SendVerificationCode(string phoneNumber);
        LoginResponse BindWechatAccount(BindWechatAccountRequest request);
    }
}