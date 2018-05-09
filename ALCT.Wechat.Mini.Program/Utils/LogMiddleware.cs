using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using ALCT.Wechat.Mini.Program.Models;

namespace ALCT.Wechat.Mini.Program.Utils
{
    public class LogMiddleware
    {
        private readonly ILogger<LogMiddleware> logger;
        private readonly RequestDelegate next;

        public LogMiddleware(ILogger<LogMiddleware> logger, RequestDelegate next)
        {
            this.logger = logger;
            this.next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            if(context.Request.GetDisplayUrl().ToLower().Contains("health")) 
            {
                await next(context);
                return;
            }

            context.Items["CorrelationId"] = Guid.NewGuid().ToString("N");
            var remoteIp = context.GetRemoteIpAddress();
            var requestTime = context.Request.Headers["RequestTime"];
            DateTime requestDatetime = DateTime.Now;
            var timestamp = 0.0;

            if (StringValues.Empty != requestTime &&
                DateTime.TryParseExact(requestTime.ToString(), "yyyy-MM-ddTHH:mm:ss", null, DateTimeStyles.None, out requestDatetime))
            {
                timestamp = DateTime.Now.Subtract(requestDatetime).TotalSeconds;
            }

            using (MemoryStream requestBodyStream = new MemoryStream())
            {
                using (MemoryStream responseBodyStream = new MemoryStream())
                {
                    Stream originalRequestBody = context.Request.Body;
                    Stream originalResponseBody = context.Response.Body;

                    try
                    {
                        string requestBodyText = "";
                        string responseBody = "";

                        if (context.Request.GetDisplayUrl().ToLower().Contains("upload") || context.Request.GetDisplayUrl().ToLower().Contains("images"))
                        {
                            requestBodyText = "Upload file, hidden the detail data in log.";
                        }
                        else
                        {
                            await context.Request.Body.CopyToAsync(requestBodyStream);
                            requestBodyStream.Seek(0, SeekOrigin.Begin);

                            requestBodyText = new StreamReader(requestBodyStream).ReadToEnd();

                            requestBodyStream.Seek(0, SeekOrigin.Begin);
                            context.Request.Body = requestBodyStream;
                        }

                        if (context.Request.GetDisplayUrl().ToLower().Contains("login"))
                        {
                            var matches = Regex.Matches(requestBodyText, "(\"Password\":?=? ?\"((?!&).)+)");
                            foreach (Match match in matches)
                            {
                                requestBodyText = requestBodyText.Replace(match.Value, "");
                            }
                        }

                        logger.LogInformation($"{timestamp} seconds, from {remoteIp}, start {context.Request.Method} {context.Request.GetDisplayUrl()} request, body: " + requestBodyText);

                        context.Response.Body = responseBodyStream;
                        Stopwatch watch = Stopwatch.StartNew();
                        await next(context);
                        watch.Stop();

                        // if (context.Request.GetDisplayUrl().ToLower().Contains("upload") || context.Request.GetDisplayUrl().ToLower().Contains("images"))
                        // {
                        //     responseBody = "Download file, hidden the detail data in log.";
                        // }
                        // else
                        {
                            responseBodyStream.Seek(0, SeekOrigin.Begin);
                            responseBody = new StreamReader(responseBodyStream).ReadToEnd();
                        }

                        responseBodyStream.Seek(0, SeekOrigin.Begin);

                        await responseBodyStream.CopyToAsync(originalResponseBody);

                        logger.LogInformation($"Request completed used {watch.Elapsed.TotalSeconds} seconds with status code: {context.Response.StatusCode}, body:" + responseBody);
                    }
                    catch (Exception ex)
                    {
                        logger.LogError("Request completed with exception " + ex.GetBaseException().Message, ex);
                        throw;
                    }
                    finally
                    {
                        context.Request.Body = originalRequestBody;
                        context.Response.Body = originalResponseBody;
                    }
                }
            }
        }
    }
}