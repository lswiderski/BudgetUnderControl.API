using BudgetUnderControl.Modules.Transactions.Application.Services;
using BudgetUnderControl.Modules.Transactions.Application.Configuration.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BudgetUnderControl.Modules.Transactions.Application.Commands.Login.Authenticate
{
    internal class AuthenticateCommandHandler : ICommandHandler<AuthenticateCommand, string>
    {
        private readonly IUserService userService;

        internal AuthenticateCommandHandler(IUserService userService)
        {
            this.userService = userService;
        }

        public async Task<string> Handle(AuthenticateCommand request, CancellationToken cancellationToken)
        {
            var token = await this.userService.ValidateLoginAsync(request.Username, request.Password);

            return token;
        }
}
}
