using BudgetUnderControl.Domain;
using BudgetUnderControl.Modules.Transactions.Application.Configuration.Commands;
using BudgetUnderControl.Modules.Transactions.Application.Contracts;
using BudgetUnderControl.Shared.Infrastructure.UnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BudgetUnderControl.Modules.Transactions.Infrastructure.Configuration.Processing
{
    internal class UnitOfWorkCommandHandlerWithResultDecorator<T, TResult> : IRequestHandler<T, TResult>
        where T : ICommand<TResult>
    {
        private readonly IRequestHandler<T, TResult> _decorated;
        private readonly IUnitOfWork _unitOfWork;
        private readonly TransactionsContext _transactionsContext;

        public UnitOfWorkCommandHandlerWithResultDecorator(
            IRequestHandler<T, TResult> decorated,
            IUnitOfWork unitOfWork,
            TransactionsContext transactionsContext)
        {
            _decorated = decorated;
            _unitOfWork = unitOfWork;
            _transactionsContext = transactionsContext;
        }

        public async Task<TResult> Handle(T command, CancellationToken cancellationToken)
        {
            var result = await this._decorated.Handle(command, cancellationToken);

            /*
            if (command is InternalCommandBase<TResult>)
            {
                var internalCommand = await _transactionsContext.InternalCommands.FirstOrDefaultAsync(x => x.Id == command.Id, cancellationToken: cancellationToken);

                if (internalCommand != null)
                {
                    internalCommand.ProcessedDate = DateTime.UtcNow;
                }
            }*/

            await this._unitOfWork.CommitAsync(cancellationToken);
            return result;
        }
    }
}