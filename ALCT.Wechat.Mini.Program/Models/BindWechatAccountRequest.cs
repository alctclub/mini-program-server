namespace ALCT.Wechat.Mini.Program.Models
{
    public class BindWechatAccountRequest
    {
        public string PhoneNumber {get; set;}
        public string VerificationCode {get; set;}
        public string WeiXinCode {get; set;}
    }
}