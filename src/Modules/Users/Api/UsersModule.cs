using Autofac;
using BudgetUnderControl.Modules.Users.Application;
using BudgetUnderControl.Modules.Users.Domain;
using BudgetUnderControl.Modules.Users.Infrastructure;
using BudgetUnderControl.Modules.Users.Infrastructure.Configuration;
using BudgetUnderControl.Shared.Abstractions.Modules;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

[assembly: InternalsVisibleTo("BudgetUnderControl.API")]
namespace BudgetUnderControl.Modules.Users.Api
{
    internal class UsersModule : IModule
    {
        public const string BasePath = "users-module";
        public string Name { get; } = "Users";
        public string Path => BasePath;

        public void ConfigureContainer(ContainerBuilder builder, IConfiguration configuration)
        {
            builder.RegisterModule(new UsersAutofacModule(configuration));

        }

        public void Register(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDomain();
            services.AddApplication();
            services.AddInfrastructure(configuration["usersModule:database:ConnectionString"]);
        }

        public void Use(IApplicationBuilder app)
        {
            app.UseDomain();
            app.UseApplication();
            app.UseInfrastructure();
        }
    }
}
