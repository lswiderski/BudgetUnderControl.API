using BudgetUnderControl.CommonInfrastructure;
using BudgetUnderControl.Modules.Transactions.Application.Configuration.Commands;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BudgetUnderControl.Modules.Transactions.Application.Commands.Users.UpdateUser
{
    internal class UpdateUserCommandHandler : ICommandHandler<UpdateUserCommand>
    {
        private readonly IUserService userService;

        internal UpdateUserCommandHandler(IUserService userService)
        {
            this.userService = userService;
        }

        public async Task<Unit> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            await this.userService.EditUserAsync(new CommonInfrastructure.Commands.User.EditUser
            {
                IsActivated = request.IsActivated,
                Email = request.Email,
                Role = request.Role,
                ExternalId = request.ExternalId,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Username = request.Username
            });

            return Unit.Value;
        }
    }
}