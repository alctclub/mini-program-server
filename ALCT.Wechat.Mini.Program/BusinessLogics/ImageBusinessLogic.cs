using ALCT.Wechat.Mini.Program.Databases;
using ALCT.Wechat.Mini.Program.Models;
using ALCT.Wechat.Mini.Program.Agents;

using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;

namespace ALCT.Wechat.Mini.Program.BusinessLogics
{
    public class ImageBusinessLogic : BasicBusinessLogic, IImageBusinessLogic
    {
        private string[] AllowOperates = new string[] {"pickup", "unload", "sign", "pod"};
        private readonly IImageAgent imageAgent;

        public ImageBusinessLogic(MPDbContext dbContext,
            IAuthenticationAgent authenticationAgent,
            IConfigurationService configurationService,
            IImageAgent imageAgent,
            ILogger<ShipmentBusinessLogic> logger)
        {
            this.dbContext = dbContext;
            this.authenticationAgent = authenticationAgent;
            this.configurationService = configurationService;
            this.imageAgent = imageAgent;
            this.aLCTConfiguration = configurationService.GetALCTConfiguration();
            this.logger = logger;
        }

        public BasicResponseModel UploadFile(string sessionId, ImageUploadModel request) 
        {
            if(!AllowOperates.Contains(request.ImageType)) 
            {
                return new GetFilesResponse() 
                {
                    Code = ImageErrorCode.InvalidOperation,
                    Message = "非法操作"
                };
            }

            try
            {
                var response = imageAgent.UploadImage(GetToken(sessionId).AccessToken, request.ToALCTUploadFileModel());
                if(response.Code != "0") 
                {
                    return new BasicResponseModel() 
                        {
                            Code = ImageErrorCode.UploadImageFailed,
                            Message = response.Message
                        };
                }
                return new BasicResponseModel();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Upload file failed");

                return new GetFilesResponse() 
                {
                    Code = ImageErrorCode.UploadImageFailed,
                    Message = "上传图片失败，请重试"
                };
            }
        }

        public BasicResponseModel DeleteFile(string sessionId, DeleteImageRequest request) 
        {
            if(!AllowOperates.Contains(request.ImageType)) 
            {
                return new GetFilesResponse() 
                {
                    Code = ImageErrorCode.InvalidOperation,
                    Message = "非法操作"
                };
            }

            try
            {
                var response = imageAgent.DeleteImage(GetToken(sessionId).AccessToken, request.ShipmentCode, request.FileName, request.ImageType);
                if(response.Code != "0") 
                {
                    return new BasicResponseModel() 
                        {
                            Code = ImageErrorCode.DeleteImageFailed,
                            Message = response.Message
                        };
                }
                return new BasicResponseModel();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Delete file failed");

                return new GetFilesResponse() 
                {
                    Code = ImageErrorCode.DeleteImageFailed,
                    Message = "删除图片失败，请刷新后重试"
                };
            }
        }

        public GetFilesResponse GetFiles(string sessionId, string shipmentCode, string imageType) 
        {
            if(!AllowOperates.Contains(imageType)) 
            {
                return new GetFilesResponse() 
                {
                    Code = ImageErrorCode.InvalidOperation,
                    Message = "非法操作"
                };
            }

            try
            {
                var alctResponse = imageAgent.GetImages(GetToken(sessionId).AccessToken, shipmentCode, imageType);

                var response = new GetFilesResponse();
                response.Files = alctResponse.Select(x=>new CommonFileModel()
                {
                    FileName = x.FileName,
                    FileUrl = x.FileData
                }).ToList();
                return response;
            }
            catch(Exception ex)
            {
                logger.LogError(ex, "Get file failed");
                return new GetFilesResponse() 
                {
                    Code = ImageErrorCode.GetImageFailed,
                    Message = "获取图片失败，请重试"
                };
            }
        }
    }
}