using ALCT.Wechat.Mini.Program.Models;

namespace ALCT.Wechat.Mini.Program.BusinessLogics
{
    public interface IImageBusinessLogic
    {
        BasicResponseModel UploadFile(string sessionId, ImageUploadModel request);
        BasicResponseModel DeleteFile(string sessionId, DeleteImageRequest request);
        GetFilesResponse GetFiles(string sessionId, string shipmentCode, string imageType);
    }
}