using System;

namespace ALCT.Wechat.Mini.Program.Models
{
    public class ALCTLoginResponse : ALCTBasicResponse
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public DateTime ExpiryDate {get; set;}
    }
}