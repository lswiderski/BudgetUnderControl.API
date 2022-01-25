using Autofac;
using BudgetUnderControl.Modules.Transactions.Infrastructure.Configuration.Processing;
using BudgetUnderControl.Shared.Application.CQRS.Configuration.Commands;
using BudgetUnderControl.Shared.Infrastructure.UnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetUnderControl.Modules.Users.Infrastructure.Configuration.Processing
{
    internal class ProcessingModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<UnitOfWork>()
               .As<IUnitOfWork>()
               .InstancePerLifetimeScope();

            builder.RegisterGenericDecorator(
                typeof(ValidationCommandHandlerWithResultDecorator<,>),
                typeof(IRequestHandler<,>),
                x => x.ImplementationType.IsClosedTypeOf(typeof(ICommandHandler<>)) || x.ImplementationType.IsClosedTypeOf(typeof(ICommandHandler<,>)));

            builder.RegisterGenericDecorator(
                typeof(UnitOfWorkCommandHandlerWithResultDecorator<,>),
                typeof(IRequestHandler<,>),
                x => x.ImplementationType.IsClosedTypeOf(typeof(ICommandHandler<>)) || x.ImplementationType.IsClosedTypeOf(typeof(ICommandHandler<,>)));
        }
    }
}
