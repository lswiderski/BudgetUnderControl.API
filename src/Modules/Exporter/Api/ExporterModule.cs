using System.Runtime.CompilerServices;
using Autofac;
using BudgetUnderControl.Modules.Exporter.Application;
using BudgetUnderControl.Modules.Exporter.Application.Commands.Transactions;
using BudgetUnderControl.Modules.Exporter.Application.Configuration;
using BudgetUnderControl.Modules.Exporter.Core.DTO;
using BudgetUnderControl.Shared.Abstractions.Modules;
using BudgetUnderControl.Shared.Infrastructure.Modules;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: InternalsVisibleTo("BudgetUnderControl.API")]
namespace BudgetUnderControl.Modules.Exporter.Api
{
    internal class ExporterModule: IModule
    {
        public const string BasePath = "exporter-module";
        public string Name { get; } = "Exporter";
        public string Path => BasePath;

        public void ConfigureContainer(ContainerBuilder builder, IConfiguration configuration)
        {
            builder.RegisterModule(new ExporterAutofacModule(configuration));
        }

        public void Register(IServiceCollection services, IConfiguration configuration)
        {
            services.AddApplication();
        }

        public void Use(IApplicationBuilder app)
        {
            app.UseApplication();

            app.UseModuleRequests()
                .Subscribe<GetTransactionsQuery, TransactionsReport>("export/getTransactions",
                    (query, sp) => sp.GetRequiredService<IExporterModule>().ExecuteQueryAsync(query));


        }
    }
}
