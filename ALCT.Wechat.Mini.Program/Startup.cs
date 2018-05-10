using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NLog.Extensions.Logging;
using NLog.Web;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;

using ALCT.Wechat.Mini.Program.Databases;
using ALCT.Wechat.Mini.Program.BusinessLogics;
using ALCT.Wechat.Mini.Program.Agents;
using ALCT.Wechat.Mini.Program.Models;

namespace ALCT.Wechat.Mini.Program
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        private ILoggerFactory loggerFactory;

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder ()
                .SetBasePath (env.ContentRootPath)
                .AddJsonFile ("appsettings.json", optional : false, reloadOnChange : true)
                .AddJsonFile ($"appsettings.{env.EnvironmentName}.json", optional : true)
                .AddEnvironmentVariables ();
            env.ConfigureNLog ("nlog.config");
            Configuration = builder.Build();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddMvc().AddJsonOptions(options => options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver());
            services.AddOptions();
            
            var serviceProvider = services.BuildServiceProvider();
            this.loggerFactory = serviceProvider.GetService<ILoggerFactory>();
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();
            loggerFactory.AddNLog();

            services.AddDbContext<MPDbContext>(builder => 
            {
                builder.UseLoggerFactory(loggerFactory);
                builder.UseMySql(Configuration.GetValue<string>("ConnectionString"));
            });

            services.AddScoped<IAuthenticationBusinessLogic, AuthenticationBusinessLogic>();
            services.AddScoped<IShipmentBusinessLogic, ShipmentBusinessLogic>();
            services.AddScoped<IInvoiceBusinessLogic, InvoiceBusinessLogic>();
            services.AddScoped<IImageBusinessLogic, ImageBusinessLogic>();
            services.AddScoped<IAuthenticationAgent, AuthenticationAgent>();
            services.AddScoped<IDriverInvoiceAgent, DriverInvoiceAgent>();
            services.AddScoped<IShipmentAgent, ShipmentAgent>();
            services.AddScoped<IImageAgent, ImageAgent>();
            services.AddScoped<IConfigurationService, ConfigurationService>();
            serviceProvider = services.BuildServiceProvider();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();
            loggerFactory.AddNLog();
            this.loggerFactory = loggerFactory;
            
            app.AddNLogWeb();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc(routes =>
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action}/{id?}"));
            
            app.Map("/health", builder =>
            {
                builder.Run(async(context) =>
                {
                    await context.Response.WriteAsync("OK");
                });
            });
        }
    }
}
