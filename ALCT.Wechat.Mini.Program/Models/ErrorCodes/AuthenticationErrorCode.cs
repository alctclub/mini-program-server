namespace ALCT.Wechat.Mini.Program.Models
{
    public class AuthenticationErrorCode
    {
        public const int SystemUnhandledException = 999999;
        public const int FailedGetWxOpenId = 100000;
        public const int NewAccountNeedBind = 100001;
        public const int LoginFailed = 100002;
        public const int AccessTokenInvalid = 100003;
        public const int PhoneNumberInvalid = 100004;
        public const int InvalidVerificationCode = 100005;
    }
}