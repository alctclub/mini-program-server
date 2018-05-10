using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

using ALCT.Wechat.Mini.Program.BusinessLogics;
using ALCT.Wechat.Mini.Program.Models;

namespace ALCT.Wechat.Mini.Program.Controllers
{
    [Route("api/v1/miniprogram/images")]
    public class ImageController : BasicController
    {
        private readonly IImageBusinessLogic imageBusinessLogic;
        public ImageController(IImageBusinessLogic imageBusinessLogic)
        {
            this.imageBusinessLogic = imageBusinessLogic;
            this.basicBusinessLogic = (BasicBusinessLogic)imageBusinessLogic;
        }

        [HttpPost]
        [Route("")]
        public IActionResult UploadImage([FromForm]ImageUploadModel image)
        {            
            if(!CheckSessionId()) 
            {
                return Unauthorized();
            }
            return Ok(imageBusinessLogic.UploadFile(GetSessionId(), image));
        }

        [HttpDelete]
        [Route("{imageType}")]
        public IActionResult DeleteImage(string imageType, [FromQuery]DeleteImageRequest request)
        {
            if(!CheckSessionId()) 
            {
                return Unauthorized();
            }
            request.ImageType = imageType;
            return Ok(imageBusinessLogic.DeleteFile(GetSessionId(), request));
        }

        [HttpGet]
        [Route("{imageType}")]
        public IActionResult GetImages(string imageType, string shipmentCode) 
        {
            if(!CheckSessionId()) 
            {
                return Unauthorized();
            }

            return Ok(imageBusinessLogic.GetFiles(GetSessionId(), shipmentCode, imageType));
        }
    }
}