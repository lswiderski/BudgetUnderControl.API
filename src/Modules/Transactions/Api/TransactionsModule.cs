using System.Runtime.CompilerServices;
using Autofac;
using BudgetUnderControl.Modules.Transactions.Application;
using BudgetUnderControl.Modules.Transactions.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

[assembly: InternalsVisibleTo("BudgetUnderControl.API")]
namespace BudgetUnderControl.Modules.Transactions.Api
{
    internal static class TransactionsModule
    {
        public static IServiceCollection AddTransactionsModule(this IServiceCollection services, IContainer autofacContainer)
        {
            services.AddApplication();
            services.AddInfractructure(autofacContainer);

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
