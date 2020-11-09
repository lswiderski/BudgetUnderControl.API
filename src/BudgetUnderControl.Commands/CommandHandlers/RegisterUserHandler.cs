using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BudgetUnderControl.CommonInfrastructure.Commands
{
    public class RegisterUserHandler : ICommandHandler<RegisterUserCommand>
    {
        private readonly IUserService loginService;
        public RegisterUserHandler(IUserService loginService)
        {
            this.loginService = loginService;
        }

        public async Task HandleAsync(RegisterUserCommand command)
        {
            await this.loginService.RegisterUserAsync(command);
        }
    }
}
