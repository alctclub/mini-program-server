using ALCT.Wechat.Mini.Program.Databases;
using ALCT.Wechat.Mini.Program.Models;
using ALCT.Wechat.Mini.Program.Agents;

using Microsoft.Extensions.Logging;

namespace ALCT.Wechat.Mini.Program.BusinessLogics
{
    public class ImageBusinessLogic : BasicBusinessLogic
    {
        public ImageBusinessLogic(MPDbContext dbContext,
            IAuthenticationAgent authenticationAgent,
            ILogger<ShipmentBusinessLogic> logger)
        {
            this.dbContext = dbContext;
            this.authenticationAgent = authenticationAgent;
            this.aLCTConfiguration = new ALCTConfiguration();
            this.logger = logger;
        }
    }
}