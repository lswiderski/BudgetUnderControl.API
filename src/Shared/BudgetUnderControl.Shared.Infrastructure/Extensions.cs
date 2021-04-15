﻿using BudgetUnderControl.Shared.Abstractions.Modules;
using BudgetUnderControl.Shared.Infrastructure.Contexts;
using BudgetUnderControl.Shared.Infrastructure.Database;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Reflection;

namespace BudgetUnderControl.Shared.Infrastructure
{
    public static class Extensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services,
           IList<Assembly> assemblies, IList<IModule> modules)
        {

            services.AddSingleton<IContextFactory, ContextFactory>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient(sp => sp.GetRequiredService<IContextFactory>().Create());
            services.AddHostedService<DbMigrator>();
            return services;
        }


        public static IApplicationBuilder UseInfrastructure(this IApplicationBuilder app)
        {
            return app;
        }
    }
}
