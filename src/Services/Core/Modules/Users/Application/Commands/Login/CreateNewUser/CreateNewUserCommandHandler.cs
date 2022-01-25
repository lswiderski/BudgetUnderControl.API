
using BudgetUnderControl.Modules.Users.Application.Services;
using BudgetUnderControl.Shared.Application.CQRS.Configuration.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BudgetUnderControl.Modules.Users.Application.Commands.Login.CreateNewUser
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
            var token = await this.userService.RegisterUserAsync(request);

            return token;
        }
    }
}