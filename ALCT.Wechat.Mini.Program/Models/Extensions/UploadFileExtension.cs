using System;
using System.IO;

namespace ALCT.Wechat.Mini.Program.Models
{
    public static class UploadFileExtension
    {
        public static ALCTUploadFileModel ToALCTUploadFileModel(this ImageUploadModel model) 
        {
            if(model == null)
            {
                return null;
            }

            var alctModel = new ALCTUploadFileModel() 
            {
                ShipmentCode = model.ShipmentCode,
                ImageType = model.ImageType,
                FileName = model.MyImage.FileName,
                FileExt = Path.GetExtension(model.MyImage.FileName),
                FileData = string.Empty,
                BaiduLatitude = model.Latitude,
                BaiduLongitude = model.Longitude,
                Location = model.Location,
                ImageTakenDate = model.ImageTakenDate
            };

            using(MemoryStream ms = new MemoryStream())
            {
                model.MyImage.CopyTo(ms);
                var bytes = new byte[ms.Length];
                ms.Read(bytes, 0, bytes.Length);
                alctModel.FileData = Convert.ToBase64String(bytes);
            }

            return alctModel;
        } 
    }
}