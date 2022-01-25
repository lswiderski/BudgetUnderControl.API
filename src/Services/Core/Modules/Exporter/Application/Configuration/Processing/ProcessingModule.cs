using Autofac;
using BudgetUnderControl.Shared.Application.CQRS.Configuration.Commands;
using MediatR;

namespace BudgetUnderControl.Modules.Exporter.Application.Configuration.Processing
{
    internal class ProcessingModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterGenericDecorator(
                typeof(ValidationCommandHandlerWithResultDecorator<,>),
                typeof(IRequestHandler<,>),
                x => x.ImplementationType.IsClosedTypeOf(typeof(ICommandHandler<>)) ||
                     x.ImplementationType.IsClosedTypeOf(typeof(ICommandHandler<,>)));
        }
    }
}
