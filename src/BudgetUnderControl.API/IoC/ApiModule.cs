using Autofac;
using BudgetUnderControl.API.Extensions;
using BudgetUnderControl.Common;
using BudgetUnderControl.Domain;
using BudgetUnderControl.Modules.Transactions.Api;
using BudgetUnderControl.Shared.Infrastructure.Settings;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using NLog;
using System;
using System.IO;

namespace BudgetUnderControl.API.IoC
{
    public class ApiModule : Module
    {
        private readonly IConfiguration configuration;
        private readonly IWebHostEnvironment environment;
        public ApiModule(IConfiguration configuration, IWebHostEnvironment environment)
        {
            this.configuration = configuration;
            this.environment = environment;
        }

        protected override void Load(ContainerBuilder builder)
        {
            var settings = this.configuration.GetSettings<GeneralSettings>().InjectEnvVariables();
            Console.WriteLine("Connection string: " + settings.ConnectionString);
            Console.WriteLine("Application Type:  " + settings.ApplicationType.ToString());
            Console.WriteLine("DB Name:  " + settings.BUC_DB_Name);
            if (string.IsNullOrWhiteSpace(environment.WebRootPath))
            {
                environment.WebRootPath = System.IO.Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
            }
            settings.FileRootPath = environment.WebRootPath;
            builder.RegisterInstance(settings)
                .SingleInstance();

            //var contextConfig = new ContextConfig() { DbName = Settings.DB_SQLServer_NAME,  Application = ApplicationType.SqlServerMigrations, DbPassword= "Qwerty!1", DbUser="buc" };
            var contextConfig = new ContextConfig() { DbName = settings.BUC_DB_Name, Application = settings.ApplicationType, ConnectionString = settings.ConnectionString };

            builder.RegisterInstance(contextConfig).As<IContextConfig>();
            
            var logManager = new NLogManager();
            builder.RegisterInstance(logManager).As<ILogManager>().SingleInstance();
            builder.RegisterInstance(logManager.GetLog()).As<ILogger>();

            builder.RegisterModule(new TransactionsAutofacModule(contextConfig));

        }
    }
}
