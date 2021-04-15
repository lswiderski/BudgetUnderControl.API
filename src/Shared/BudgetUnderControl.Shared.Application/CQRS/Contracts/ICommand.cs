using MediatR;
using System;

namespace BudgetUnderControl.Shared.Application.CQRS.Contracts
{
    public interface ICommand<out TResult> : IRequest<TResult>
    {
        Guid Id { get; }
    }

    public interface ICommand : ICommand<Unit>
    {
        Guid Id { get; }
    }
}
