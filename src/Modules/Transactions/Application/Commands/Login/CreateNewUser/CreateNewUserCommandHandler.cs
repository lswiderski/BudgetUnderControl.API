using BudgetUnderControl.CommonInfrastructure;
using BudgetUnderControl.CommonInfrastructure.Commands;
using BudgetUnderControl.Modules.Transactions.Application.Configuration.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BudgetUnderControl.Modules.Transactions.Application.Commands.Login.CreateNewUser
{
    internal class CreateNewUserCommandHandler : ICommandHandler<CreateNewUserCommand, string>
    {
        private readonly IUserService userService;

        internal CreateNewUserCommandHandler(IUserService userService)
        {
            this.userService = userService;
        }

        public async Task<string> Handle(CreateNewUserCommand request, CancellationToken cancellationToken)
        {
            var token = await this.userService.RegisterUserAsync(new RegisterUserCommand
            {
                TokenId = request.TokenId,
                Email = request.Email,
                Password = request.Password,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Username = request.Username
            });

            return token;
        }
    }
}