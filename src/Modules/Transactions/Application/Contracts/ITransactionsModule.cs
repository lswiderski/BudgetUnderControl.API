using BudgetUnderControl.Shared.Application.CQRS.Contracts;
using System.Threading.Tasks;

namespace BudgetUnderControl.Modules.Transactions.Application.Contracts
{
    public interface ITransactionsModule
    {
        Task<TResult> ExecuteCommandAsync<TResult>(ICommand<TResult> command);

        Task ExecuteCommandAsync(ICommand command);

        Task<TResult> ExecuteQueryAsync<TResult>(IQuery<TResult> query);
    }
}
