using Autofac;
using BudgetUnderControl.Modules.Transactions.Infrastructure.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using BudgetUnderControl.Shared.Infrastructure.Settings;
using Autofac.Extensions.DependencyInjection;
using BudgetUnderControl.Modules.Transactions.Infrastructure.Clients;
using BudgetUnderControl.Modules.Transactions.Application.Clients.Users;
using Microsoft.EntityFrameworkCore;
using BudgetUnderControl.Domain;
using BudgetUnderControl.Modules.Transactions.Infrastructure.Profiles.Transaction;

namespace BudgetUnderControl.Modules.Transactions.Infrastructure
{
    public static class Extenstions
    {

        public static IServiceCollection AddInfractructure(this IServiceCollection services, string connectionString)
        {
            services
                 .AddAutoMapper(typeof(TransactionsFilterProfile))
                .AddSingleton<IUsersApiClient, UsersApiClient>()
                .AddDbContext<TransactionsContext>(x => x.UseSqlServer(connectionString));

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
