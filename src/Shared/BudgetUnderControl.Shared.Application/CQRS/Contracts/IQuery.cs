using MediatR;

namespace BudgetUnderControl.Shared.Application.CQRS.Contracts
{
    public interface IQuery<out TResult> : IRequest<TResult>
    {
    }
}
