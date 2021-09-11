using System.Runtime.CompilerServices;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using BudgetUnderControl.Modules.Transactions.Application;
using BudgetUnderControl.Modules.Transactions.Application.Contracts;
using BudgetUnderControl.Modules.Transactions.Application.DTO.Transaction;
using BudgetUnderControl.Modules.Transactions.Application.Services;
using BudgetUnderControl.Modules.Transactions.Application.Transactions.GetTransactions;
using BudgetUnderControl.Modules.Transactions.Infrastructure;
using BudgetUnderControl.Modules.Transactions.Infrastructure.Configuration;
using BudgetUnderControl.Modules.Transactions.Infrastructure.Services;
using BudgetUnderControl.Shared.Abstractions.Modules;
using BudgetUnderControl.Shared.Infrastructure.Modules;
using BudgetUnderControl.Shared.Infrastructure.Settings;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: InternalsVisibleTo("BudgetUnderControl.API")]
namespace BudgetUnderControl.Modules.Transactions.Api
{
    internal class TransactionsModule : IModule
    {
        public const string BasePath = "transactions-module";
        public string Name { get; } = "Transactions";
        public string Path => BasePath;

        public void ConfigureContainer(ContainerBuilder builder, IConfiguration configuration)
        {
            builder.RegisterModule(new TransactionsAutofacModule(configuration));
           
        }

        public void Register(IServiceCollection services, IConfiguration configuration)
        {
            services.AddApplication();
            services.AddInfractructure(configuration["transactionsModule:database:ConnectionString"]);
        }

        public void Use(IApplicationBuilder app)
        {
            app.UseApplication();
            app.UseInfrastructure();
            
            app.UseModuleRequests()
                .Subscribe<GetTransactionsQuery, TransactionListDataSource> ("transactions/get",
                (query, sp) => sp.GetRequiredService<ITransactionsModule>().ExecuteQueryAsync(query));
        }
    }
}
