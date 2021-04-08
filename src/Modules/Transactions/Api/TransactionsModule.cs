using System.Runtime.CompilerServices;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using BudgetUnderControl.Modules.Transactions.Application;
using BudgetUnderControl.Modules.Transactions.Application.Services;
using BudgetUnderControl.Modules.Transactions.Infrastructure;
using BudgetUnderControl.Modules.Transactions.Infrastructure.Configuration;
using BudgetUnderControl.Modules.Transactions.Infrastructure.Services;
using BudgetUnderControl.Shared.Abstractions.Modules;
using BudgetUnderControl.Shared.Infrastructure.Settings;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

[assembly: InternalsVisibleTo("BudgetUnderControl.API")]
namespace BudgetUnderControl.Modules.Transactions.Api
{
    internal class TransactionsModule : IModule
    {
        public const string BasePath = "transactions-module";
        public string Name { get; } = "Transactions";
        public string Path => BasePath;

        public void ConfigureContainer(ContainerBuilder builder, GeneralSettings settings)
        {
            builder.RegisterModule(new TransactionsAutofacModule(settings));
        }

        public void Register(IServiceCollection services)
        {
            services.AddApplication();
            services.AddInfractructure();
        }

        public void Use(IApplicationBuilder app)
        {
            app.UseApplication();
            app.UseInfrastructure();
        }
    }
}
