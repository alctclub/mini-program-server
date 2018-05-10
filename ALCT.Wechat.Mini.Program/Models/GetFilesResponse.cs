using System.Collections.Generic;

using ALCT.Wechat.Mini.Program.Models;

namespace ALCT.Wechat.Mini.Program.Models
{
    public class GetFilesResponse : BasicResponseModel
    {
        public IList<CommonFileModel> Files {get; set;}
    }
}