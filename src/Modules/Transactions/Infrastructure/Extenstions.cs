using Autofac;
using BudgetUnderControl.Modules.Transactions.Infrastructure.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace BudgetUnderControl.Modules.Transactions.Infrastructure
{
    public static class Extenstions
    {

        public static IServiceCollection AddInfractructure(this IServiceCollection services, IContainer autofacContainer)
        {
            TransactionsCompositionRoot.SetContainer(autofacContainer);

            return services;
        }

        public static IApplicationBuilder UseInfrastructure(this IApplicationBuilder app)
        {
            return app;
        }
    }
}
