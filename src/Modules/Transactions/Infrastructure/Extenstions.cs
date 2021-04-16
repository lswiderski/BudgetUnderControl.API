using Autofac;
using BudgetUnderControl.Modules.Transactions.Infrastructure.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using BudgetUnderControl.Shared.Infrastructure.Settings;
using Autofac.Extensions.DependencyInjection;
using BudgetUnderControl.Modules.Transactions.Infrastructure.Clients;
using BudgetUnderControl.Modules.Transactions.Application.Clients.Users;

namespace BudgetUnderControl.Modules.Transactions.Infrastructure
{
    public static class Extenstions
    {

        public static IServiceCollection AddInfractructure(this IServiceCollection services)
        {
            services
                .AddSingleton<IUsersApiClient, UsersApiClient>();

            return services;
        }

        public static IApplicationBuilder UseInfrastructure(this IApplicationBuilder app)
        {
            var scope = app.ApplicationServices.GetAutofacRoot();
            TransactionsCompositionRoot.SetScope(scope);
            return app;
        }
    }
}
