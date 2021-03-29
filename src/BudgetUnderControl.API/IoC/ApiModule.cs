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
        public ApiModule()
        {
        }

        protected override void Load(ContainerBuilder builder)
        {
         
            var logManager = new NLogManager();
            builder.RegisterInstance(logManager).As<ILogManager>().SingleInstance();
            builder.RegisterInstance(logManager.GetLog()).As<ILogger>();
            
        }
    }
}
