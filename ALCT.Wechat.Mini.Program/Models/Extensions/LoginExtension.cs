using System;

namespace ALCT.Wechat.Mini.Program.Models
{
    public static class LoginExtension
    {
        public static Token ToToken(this ALCTLoginResponse response, string openId) 
        {
            if(response == null) 
            {
                return null;
            }

            return new Token()
            {
                OpenId = openId,
                AccessToken = response.Token,
                RefreshToken = response.RefreshToken,
                ExpiryDate = response.ExpiryDate.AddSeconds(-1800),
                CreatedDate = DateTime.Now
            };
        }
    }
}