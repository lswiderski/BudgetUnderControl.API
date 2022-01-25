using Autofac;
using BudgetUnderControl.Modules.Users.Application;
using BudgetUnderControl.Modules.Users.Application.Commands.Users.GetUserIdentity;
using BudgetUnderControl.Modules.Users.Application.Contracts;
using BudgetUnderControl.Modules.Users.Application.Services;
using BudgetUnderControl.Modules.Users.Domain;
using BudgetUnderControl.Modules.Users.Infrastructure;
using BudgetUnderControl.Modules.Users.Infrastructure.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Runtime.CompilerServices;
using BudgetUnderControl.Shared.Abstractions.Modules;
using BudgetUnderControl.Shared.Infrastructure.Modules;

[assembly: InternalsVisibleTo("BudgetUnderControl.Core")]
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

            app.UseModuleRequests()
               .Subscribe<GetUserIdentityQuery, IUserIdentityContext>("users/get",
                   (query, sp) => sp.GetRequiredService<IUsersModule>().ExecuteQueryAsync(query));
        }
    }
}
