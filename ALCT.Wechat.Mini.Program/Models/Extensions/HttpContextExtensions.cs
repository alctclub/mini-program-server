using Microsoft.AspNetCore.Http;
using System.Linq;

namespace ALCT.Wechat.Mini.Program.Models
{
    public static class HttpContextExtensions
    {
        public static string GetRemoteIpAddress(this HttpContext context)
        {
            var ip = context.Request.Headers["X-Forwarded-For"].FirstOrDefault();
            if (string.IsNullOrEmpty(ip))
            {
                ip = context.Connection.RemoteIpAddress.ToString();
            }
            return string.IsNullOrEmpty(ip) ? "*.*.*.*" : ip;
        }
    }
}