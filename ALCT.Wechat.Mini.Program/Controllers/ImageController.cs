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
        public ImageController() 
        {

        }

        [HttpPost]
        [Route("")]
        public IActionResult UploadImage([FromForm]ImageUploadModel image)
        {
            
            return Ok();
        }

        [HttpDelete]
        [Route("{operation}")]
        public IActionResult DeleteImage(string operation, [FromQuery]DeleteImageRequest request)
        {

            return Ok();
        }

        [HttpGet]
        [Route("{operation}")]
        public IActionResult GetImages(string operation, string orderCode, string shipmentCode) 
        {
            
            return Ok();
        }
    }
}