using ALCT.Wechat.Mini.Program.Models;

namespace ALCT.Wechat.Mini.Program.BusinessLogics
{
    public interface IBasicBusinessLogic
    {
         Token GetToken(string sessionId);
    }
}