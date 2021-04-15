using Autofac;
using BudgetUnderControl.Domain;
using BudgetUnderControl.Modules.Users.Application.Contracts;
using BudgetUnderControl.Modules.Users.Application.Services;
using BudgetUnderControl.Modules.Users.Infrastructure.Configuration.Mediation;
using BudgetUnderControl.Modules.Users.Infrastructure.Configuration.Processing;
using BudgetUnderControl.Modules.Users.Infrastructure.DataAccess;
using BudgetUnderControl.Modules.Users.Infrastructure.Services;
using BudgetUnderControl.Shared.Infrastructure.Configuration;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetUnderControl.Modules.Users.Infrastructure.Configuration
{
    public class UsersAutofacModule : Autofac.Module
    {
        private readonly IConfiguration configuration;
        public UsersAutofacModule(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<UsersModuleExecutor>()
              .As<IUsersModule>()
              .InstancePerLifetimeScope();

            var contextConfig = new ContextConfig() { DbName = configuration["transactionsModule:database:BUC_DB_Name"], ConnectionString = configuration["transactionsModule:database:ConnectionString"] };

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
