using System.Runtime.CompilerServices;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using BudgetUnderControl.Modules.Transactions.Application;
using BudgetUnderControl.Modules.Transactions.Infrastructure;
using BudgetUnderControl.Shared.Infrastructure.Settings;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

[assembly: InternalsVisibleTo("BudgetUnderControl.API")]
namespace BudgetUnderControl.Modules.Transactions.Api
{
    internal static class TransactionsModule
    {
        public static IServiceCollection AddTransactionsModule(this IServiceCollection services)
        {
            services.AddApplication();
            services.AddInfractructure();

            return services;
        }

        public static IApplicationBuilder UseTransactionsModule(this IApplicationBuilder app)
        {
            app.UseApplication();
            app.UseInfrastructure();

            return app;
        }

    }
}
