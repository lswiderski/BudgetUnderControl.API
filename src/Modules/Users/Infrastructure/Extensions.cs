using Autofac.Extensions.DependencyInjection;
using BudgetUnderControl.Domain;
using BudgetUnderControl.Modules.Users.Infrastructure.Configuration;
using BudgetUnderControl.Modules.Users.Profiles;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using AutoMapper;
using BudgetUnderControl.Modules.Users.Infrastructure.Clients;
using BudgetUnderControl.Modules.Users.Infrastructure.Clients.Requests;
using BudgetUnderControl.Modules.Users.Infrastructure.DataAccess;

namespace BudgetUnderControl.Modules.Users.Infrastructure
{
    public static class Extensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, string connectionString)
        {
            services

               .AddAutoMapper(typeof(UserProfile))
               .AddSingleton<INotificationsApiClient, NotificationsApiClient>()
               .AddDbContext<UsersDbContext>(x => x.UseSqlServer(connectionString));

            return services;
        }

        public static IApplicationBuilder UseInfrastructure(this IApplicationBuilder app)
        {
            var scope = app.ApplicationServices.GetAutofacRoot();
            UsersCompositionRoot.SetScope(scope);
            return app;
        }

    }
}
