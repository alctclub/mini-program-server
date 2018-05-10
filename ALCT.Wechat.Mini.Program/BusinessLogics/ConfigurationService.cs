using ALCT.Wechat.Mini.Program.Models;
using ALCT.Wechat.Mini.Program.Databases;

using System.Linq;
using Newtonsoft.Json;
using Microsoft.Extensions.Logging;

namespace ALCT.Wechat.Mini.Program.BusinessLogics
{
    public class ConfigurationService : IConfigurationService
    {
        private static ALCTConfiguration aLCTConfiguration;
        private static WechatConfiguration wechatConfiguration;
        
        private readonly MPDbContext dbContext;
        private readonly ILogger logger;

        public ConfigurationService(MPDbContext dbContext,
            ILogger<ConfigurationService> logger)
        {
            this.dbContext = dbContext;
            this.logger = logger;
        }
        
        public ALCTConfiguration GetALCTConfiguration()
        {
            if(aLCTConfiguration != null)
            {
                return aLCTConfiguration;
            }

            var config = dbContext.Set<SystemConfig>().Where(x=>x.Key == "ALCTConfiguration").FirstOrDefault();
            if(config != null)
            {
                aLCTConfiguration = JsonConvert.DeserializeObject<ALCTConfiguration>(config.Value);
            }
            else 
            {
                logger.LogError("ALCT configuration is not configed in database");
                aLCTConfiguration = new ALCTConfiguration();
            }

            return aLCTConfiguration;
        }

        public WechatConfiguration GetWechatConfiguration()
        {
            if(wechatConfiguration != null) 
            {
                return wechatConfiguration;
            }

            var config = dbContext.Set<SystemConfig>().Where(x=>x.Key == "WechatConfiguration").FirstOrDefault();
            if(config != null)
            {
                wechatConfiguration = JsonConvert.DeserializeObject<WechatConfiguration>(config.Value);
            }
            else 
            {
                logger.LogError("Wechat configuration is not configed in database");
                wechatConfiguration = new WechatConfiguration();
            }
            return wechatConfiguration;
        }
    }
}