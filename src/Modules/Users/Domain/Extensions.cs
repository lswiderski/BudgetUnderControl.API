using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetUnderControl.Modules.Users.Domain
{
    public static class Extenstions
    {

        public static IServiceCollection AddDomain(this IServiceCollection services)
        {

            return services;
        }

        public static IApplicationBuilder UseDomain(this IApplicationBuilder app)
        {
            return app;
        }
    }
}
