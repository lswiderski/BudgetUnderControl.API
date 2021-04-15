using Autofac.Extensions.DependencyInjection;
using BudgetUnderControl.Domain;
using BudgetUnderControl.Modules.Users.Infrastructure.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetUnderControl.Modules.Users.Infrastructure
{
    public static class Extensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, string connectionString)
            => services
            .AddDbContext<UsersDbContext>(x => x.UseSqlServer(connectionString));

        public static IApplicationBuilder UseInfrastructure(this IApplicationBuilder app)
        {
            var scope = app.ApplicationServices.GetAutofacRoot();
            UsersCompositionRoot.SetScope(scope);
            return app;
        }

    }
}
