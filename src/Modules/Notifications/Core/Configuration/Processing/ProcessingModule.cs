using Autofac;
using BudgetUnderControl.Shared.Infrastructure.UnitOfWork;

namespace BudgetUnderControl.Modules.Notifications.Core.Configuration.Processing
{
    internal class ProcessingModule: Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<UnitOfWork>()
                .As<IUnitOfWork>()
                .InstancePerLifetimeScope();

          
        }
    }
}
