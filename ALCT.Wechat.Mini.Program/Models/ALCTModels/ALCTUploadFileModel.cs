using System;

namespace ALCT.Wechat.Mini.Program.Models
{
    public class ALCTUploadFileModel
    {
        public string ShipmentCode {get; set;}

        public string ImageType {get; set;}

        public string FileName {get; set;}
        
        public string FileExt {get; set;}

        public string FileData {get; set;}

        public decimal? BaiduLongitude { get; set; }

        public decimal? BaiduLatitude { get; set; }

        public decimal? Speed { get; set; }

        public decimal? Direction { get; set; }

        public decimal? Altitude { get; set; }

        public string Location {get; set;}

        public DateTime ImageTakenDate { get; set; }
        public string LocationType {get; set;} = "GPS";
    }
}