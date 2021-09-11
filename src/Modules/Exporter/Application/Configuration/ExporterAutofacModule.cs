using Autofac;
using BudgetUnderControl.Modules.Exporter.Application.Configuration.Mediation;
using BudgetUnderControl.Modules.Exporter.Application.Configuration.Processing;
using BudgetUnderControl.Modules.Exporter.Core.Services;
using BudgetUnderControl.Modules.Exporter.Targets.CSV.Creators.Transactions;
using BudgetUnderControl.Modules.Exporter.Targets.Excel.Builders.Transactions;
using BudgetUnderControl.Shared.Abstractions.Enums.Export;
using BudgetUnderControl.Shared.Infrastructure.Configuration;
using Microsoft.Extensions.Configuration;

namespace BudgetUnderControl.Modules.Exporter.Application.Configuration
{
    public class ExporterAutofacModule : Autofac.Module
    {
        private readonly IConfiguration configuration;
        public ExporterAutofacModule(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ExporterModuleExecutor>()
              .As<IExporterModule>()
              .InstancePerLifetimeScope();
            
            builder.RegisterType<TransactionsCSVCreator>()
                .Keyed<ITransacationsReportCreator>(ExportFileType.CSV)
                .InstancePerLifetimeScope();
            builder.RegisterType<TransactionsExcelCreator>()
                .Keyed<ITransacationsReportCreator>(ExportFileType.Excel)
                .InstancePerLifetimeScope();
          
            var typeNamesEndings = new string[] { "Repository", "Service", "Builder" };

            foreach (var typeNameEnding in typeNamesEndings)
            {
                builder.RegisterAssemblyTypes(Assemblies.Application)
                    .Where(type => type.Name.EndsWith(typeNameEnding))
                    .AsImplementedInterfaces()
                    .InstancePerLifetimeScope()
                    .FindConstructorsWith(new AllConstructorFinder());
            }
            
            builder.RegisterModule(new MediatorModule());
            builder.RegisterModule(new ProcessingModule());

        }
    }
}
