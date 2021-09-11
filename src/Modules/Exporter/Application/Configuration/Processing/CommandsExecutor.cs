using Autofac;
using BudgetUnderControl.Shared.Application.CQRS.Contracts;
using MediatR;
using System.Threading.Tasks;

namespace BudgetUnderControl.Modules.Exporter.Application.Configuration.Processing
{
    internal static class CommandsExecutor
    {
        internal static async Task Execute(ICommand command)
        {
            using (var scope = ExporterCompositionRoot.BeginLifetimeScope())
            {
                var mediator = scope.Resolve<IMediator>();
                await mediator.Send(command);
            }
        }

        internal static async Task<TResult> Execute<TResult>(ICommand<TResult> command)
        {
            using (var scope = ExporterCompositionRoot.BeginLifetimeScope())
            {
                var mediator = scope.Resolve<IMediator>();
                return await mediator.Send(command);
            }
        }
    }
}
