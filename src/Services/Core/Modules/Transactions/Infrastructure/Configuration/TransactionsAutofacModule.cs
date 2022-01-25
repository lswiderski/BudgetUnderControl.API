using Autofac;
using BudgetUnderControl.Modules.Transactions.Application.Services;
using BudgetUnderControl.Domain;
using BudgetUnderControl.Modules.Transactions.Infrastructure.Configuration.Mediation;
using BudgetUnderControl.Modules.Transactions.Infrastructure.Configuration.Processing;
using BudgetUnderControl.Modules.Transactions.Infrastructure.DataAccess;
using BudgetUnderControl.Modules.Transactions.Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using System.Linq;
using BudgetUnderControl.Modules.Transactions.Application.Contracts;
using BudgetUnderControl.Shared.Infrastructure.Configuration;

namespace BudgetUnderControl.Modules.Transactions.Infrastructure.Configuration
{
    public class TransactionsAutofacModule : Autofac.Module
    {
        private readonly IConfiguration configuration;
        public TransactionsAutofacModule(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<TransactionsModuleExecutor>()
              .As<ITransactionsModule>()
              .InstancePerLifetimeScope();

            var contextConfig = new ContextConfig() { ConnectionString = configuration["transactionsModule:database:ConnectionString"]};

            builder.RegisterInstance(contextConfig).As<IContextConfig>();

            var typeNamesEndings = new string[] { "Repository", "Service", "Builder" };

            foreach (var typeNameEnding in typeNamesEndings)
            {
                builder.RegisterAssemblyTypes(Assemblies.Infrastructure)
                    .Where(type => type.Name.EndsWith(typeNameEnding))
                    .AsImplementedInterfaces()
                    .InstancePerLifetimeScope()
                    .FindConstructorsWith(new AllConstructorFinder());
            }
         
            builder.RegisterType<TestDataSeeder>().As<ITestDataSeeder>().InstancePerLifetimeScope();
            builder.RegisterType<Synchroniser>().As<ISynchroniser>().InstancePerLifetimeScope();

            builder.RegisterModule(new DataAccessModule(contextConfig));
            builder.RegisterModule(new MediatorModule());
            builder.RegisterModule(new ProcessingModule());

        }

    }
}
