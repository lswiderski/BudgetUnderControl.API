using System.Threading.Tasks;
using Autofac;
using BudgetUnderControl.Modules.Exporter.Application.Configuration;
using BudgetUnderControl.Modules.Exporter.Application.Configuration.Processing;
using BudgetUnderControl.Shared.Application.CQRS.Contracts;
using MediatR;

namespace BudgetUnderControl.Modules.Exporter.Application
{
    public class ExporterModuleExecutor : IExporterModule
    {
        public async Task<TResult> ExecuteCommandAsync<TResult>(ICommand<TResult> command)
        {
            return await CommandsExecutor.Execute(command);
        }

        public async Task ExecuteCommandAsync(ICommand command)
        {
            await CommandsExecutor.Execute(command);
        }

        public async Task<TResult> ExecuteQueryAsync<TResult>(IQuery<TResult> query)
        {
            using (var scope = ExporterCompositionRoot.BeginLifetimeScope())
            {
                var mediator = scope.Resolve<IMediator>();

                return await mediator.Send(query);
            }
        }
    }
}
