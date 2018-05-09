using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.FileProviders;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using ALCT.Wechat.Mini.Program.Models;

namespace ALCT.Wechat.Mini.Program.Utils
{
    public class MPExceptionFilterAttribute : ExceptionFilterAttribute
    {
        private readonly IHostingEnvironment hostingEnvironment;
        private readonly IModelMetadataProvider modelMetadataProvider;
        private readonly ILogger logger;
        private ErrorMessageLoader messageLoader;

        public MPExceptionFilterAttribute(
            IHostingEnvironment hostingEnvironment,
            IModelMetadataProvider modelMetadataProvider,
            ErrorMessageLoader messageLoader,
            ILogger<MPExceptionFilterAttribute> logger)
        {
            this.hostingEnvironment = hostingEnvironment;
            this.modelMetadataProvider = modelMetadataProvider;
            this.messageLoader = messageLoader;
            this.logger = logger;
        }

        public override void OnException(ExceptionContext context)
        {
            logger.LogError(context.Exception, context.Exception.GetBaseException().Message);

            context.ExceptionHandled = true;
            Exception exception = context.Exception;
            if(exception is ViewBaseException) 
            {
            } else 
            {
                exception = new ViewBaseException(ExceptionErrorCode.SystemUnhandledErrorCode);
            }

            var result = new BadRequestObjectResult(GenerateErrorMessageData((ViewBaseException)exception));

            logger.LogInformation("Service action exception happened: " + JsonConvert.SerializeObject(result));
            context.Result = result;                   
        }

        private IDictionary<string,string> GenerateErrorMessageData(ViewBaseException exception) 
        {
            var data = new Dictionary<string,string>();
            data.Add("Code", exception.ErrorCode);
            data.Add("Message", string.Format(messageLoader.GetMessage(exception.ErrorCode), exception.Parameters.ToArray()));
            return data;
        }
    }
}