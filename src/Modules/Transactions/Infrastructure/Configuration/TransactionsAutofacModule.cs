using Autofac;
using BudgetUnderControl.ApiInfrastructure.Repositories;
using BudgetUnderControl.Modules.Transactions.Application.Services;
using BudgetUnderControl.Domain;
using BudgetUnderControl.Domain.Repositiories;
using BudgetUnderControl.Infrastructure;
using BudgetUnderControl.Infrastructure.Repositories;
using BudgetUnderControl.Infrastructure.Services;
using BudgetUnderControl.Shared.Application.CQRS.Contracts;
using BudgetUnderControl.Modules.Transactions.Application.Services;
using BudgetUnderControl.Modules.Transactions.Infrastructure.Configuration.Mediation;
using BudgetUnderControl.Modules.Transactions.Infrastructure.Configuration.Processing;
using BudgetUnderControl.Modules.Transactions.Infrastructure.DataAccess;
using BudgetUnderControl.Modules.Transactions.Infrastructure.Services;
using BudgetUnderControl.Shared.Infrastructure.Settings;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

            var contextConfig = new ContextConfig() { DbName = configuration["transactionsModule:database:BUC_DB_Name"], ConnectionString = configuration["transactionsModule:database:ConnectionString"]};

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
            builder.RegisterType<Encrypter>().As<IEncrypter>().InstancePerLifetimeScope();

            builder.Register<Func<IUserIdentityContext>>(c =>
            {
                var context = c.Resolve<IComponentContext>();
                return () =>
                {
                    var service = context.Resolve<IUserService>();
                    var httpAccessor = context.Resolve<IHttpContextAccessor>();
                    var identity = service.CreateUserIdentityContext(httpAccessor.HttpContext.User.Identity.Name);
                    return identity;
                };
            });

            builder.Register<IUserIdentityContext>(c =>
            {
                return c.Resolve<Func<IUserIdentityContext>>()();
            });

            builder.RegisterModule(new DataAccessModule(contextConfig));
            builder.RegisterModule(new MediatorModule());
            builder.RegisterModule(new ProcessingModule());

        }

    }
}
