using System.Threading.Tasks;
using Autofac;
using BudgetUnderControl.Modules.Notifications.Core.Configuration;
using BudgetUnderControl.Modules.Notifications.Core.Configuration.Mediation;
using BudgetUnderControl.Modules.Notifications.Core.Configuration.Processing;
using BudgetUnderControl.Shared.Application.CQRS.Contracts;
using BudgetUnderControl.Shared.Infrastructure.Configuration;
using Microsoft.Extensions.Configuration;

namespace BudgetUnderControl.Modules.Notifications.Core
{
    public class NotificationsAutofacModule: Autofac.Module
    {
        private readonly IConfiguration configuration;
        public NotificationsAutofacModule(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<NotificationsModuleExecutor>()
                .As<INotificationsModule>()
                .InstancePerLifetimeScope();

          
            var typeNamesEndings = new string[] { "Repository", "Service", "Builder" };

            foreach (var typeNameEnding in typeNamesEndings)
            {
                builder.RegisterAssemblyTypes(Assemblies.Core)
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
