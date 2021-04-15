using BudgetUnderControl.Shared.Application.CQRS.Contracts;
using MediatR;


namespace BudgetUnderControl.Shared.Application.CQRS.Configuration.Queries
{
    public interface IQueryHandler<in TQuery, TResult> :
       IRequestHandler<TQuery, TResult>
       where TQuery : IQuery<TResult>
    {
    }
}
