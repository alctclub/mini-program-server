using ALCT.Wechat.Mini.Program.Models;

namespace ALCT.Wechat.Mini.Program.Agents
{
    public interface IAuthenticationAgent
    {
         ALCTLoginResponse DoDriverLogin(ALCTDriverLoginRequest request);
         ALCTLoginResponse DoApiLogin(ALCTApiLoginRequest request);
         ALCTBasicResponse SendVerificationCode(string phoneNumber, string accessToken);
         ALCTVerifyVerificationCodeResponse VerifyVerificationCode(string phoneNumber, string code, string accessToken);
    }
}