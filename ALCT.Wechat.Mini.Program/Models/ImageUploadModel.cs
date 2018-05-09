using System;
using Microsoft.AspNetCore.Http;

namespace ALCT.Wechat.Mini.Program.Models
{
    public class ImageUploadModel
    {
        public string OrderCode {get; set;}
        public string ShipmentCode {get; set;}
        public string FileName {get; set;}
        public string FileExt {get; set;}
        public decimal? Longitude {get; set;}
        public decimal? Latitude {get; set;}
        public DateTime ImageTakenDate {get; set;}
        public string Location{get; set;}
        public string ImageType {get; set;}
        public IFormFile MyImage {get; set;}
    }
}