using System.Threading.Tasks;
using Autofac;
using BudgetUnderControl.Modules.Files.Core.Configuration;
using BudgetUnderControl.Modules.Files.Core.Configuration.Processing;
using BudgetUnderControl.Shared.Application.CQRS.Contracts;
using MediatR;

namespace BudgetUnderControl.Modules.Files.Core
{
    public class FilesModuleExecutor: IFilesModule
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
            using (var scope = FilesCompositionRoot.BeginLifetimeScope())
            {
                var mediator = scope.Resolve<IMediator>();

                return await mediator.Send(query);
            }
        }
    }
}