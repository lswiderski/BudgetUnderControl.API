using BudgetUnderControl.Domain;
using BudgetUnderControl.Shared.Application.CQRS.Configuration.Commands;
using BudgetUnderControl.Shared.Application.CQRS.Contracts;
using BudgetUnderControl.Shared.Infrastructure.UnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BudgetUnderControl.Modules.Users.Infrastructure.DataAccess;

namespace BudgetUnderControl.Modules.Users.Infrastructure.Configuration.Processing
{
    internal class UnitOfWorkCommandHandlerWithResultDecorator<T, TResult> : IRequestHandler<T, TResult>
        where T : ICommand<TResult>
    {
        private readonly IRequestHandler<T, TResult> _decorated;
        private readonly IUnitOfWork _unitOfWork;
        private readonly UsersDbContext _usersContext;

        public UnitOfWorkCommandHandlerWithResultDecorator(
            IRequestHandler<T, TResult> decorated,
            IUnitOfWork unitOfWork,
            UsersDbContext usersContext)
        {
            _decorated = decorated;
            _unitOfWork = unitOfWork;
            _usersContext = usersContext;
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