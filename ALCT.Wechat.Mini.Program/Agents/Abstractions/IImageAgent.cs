using System.Collections.Generic;
using ALCT.Wechat.Mini.Program.Models;

namespace ALCT.Wechat.Mini.Program.Agents
{
    public interface IImageAgent
    {
         ALCTBasicResponse UploadImage(string token, ALCTUploadFileModel request);
         ALCTBasicResponse DeleteImage(string token, string shipmentCode, string fileName, string imageType);
         IList<ALCTGetFileDataModel> GetImages(string token, string shipmentCode, string imageType);
    }
}