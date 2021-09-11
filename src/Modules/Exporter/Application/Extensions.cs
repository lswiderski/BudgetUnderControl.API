using Autofac.Extensions.DependencyInjection;
using BudgetUnderControl.Modules.Exporter.Application.Clients.Transactions;
using BudgetUnderControl.Modules.Exporter.Application.Configuration;
using BudgetUnderControl.Modules.Exporter.Core.Clients.Transactions;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace BudgetUnderControl.Modules.Exporter.Application
{
    public static class Extenstions
    {

        public static IServiceCollection AddApplication(this IServiceCollection services)
        {

            return services
                .AddSingleton<ITransactionsApiClient, TransactionsApiClient>();
        }

        public static IApplicationBuilder UseApplication(this IApplicationBuilder app)
        {
            var scope = app.ApplicationServices.GetAutofacRoot();
            ExporterCompositionRoot.SetScope(scope);
            return app;
        }
    }
}