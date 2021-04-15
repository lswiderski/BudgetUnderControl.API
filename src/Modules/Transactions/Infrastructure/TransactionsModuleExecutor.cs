using Autofac;
using BudgetUnderControl.Shared.Application.CQRS.Contracts;
using BudgetUnderControl.Modules.Transactions.Infrastructure.Configuration;
using BudgetUnderControl.Modules.Transactions.Infrastructure.Configuration.Processing;
using MediatR;
using System.Threading.Tasks;
using BudgetUnderControl.Modules.Transactions.Application.Contracts;

namespace BudgetUnderControl.Modules.Transactions.Infrastructure
{
    public class TransactionsModuleExecutor : ITransactionsModule
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
            using (var scope = TransactionsCompositionRoot.BeginLifetimeScope())
            {
                var mediator = scope.Resolve<IMediator>();

                return await mediator.Send(query);
            }
        }
    }
}
