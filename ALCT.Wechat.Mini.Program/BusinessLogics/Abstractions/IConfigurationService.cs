using ALCT.Wechat.Mini.Program.Models;

namespace ALCT.Wechat.Mini.Program.BusinessLogics
{
    public interface IConfigurationService
    {
        ALCTConfiguration GetALCTConfiguration();
        WechatConfiguration GetWechatConfiguration();
    }
}