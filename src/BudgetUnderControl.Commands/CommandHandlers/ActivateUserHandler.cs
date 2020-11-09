using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BudgetUnderControl.CommonInfrastructure.Commands
{
    public class ActivateUserHandler : ICommandWithResultHandler<ActivateUserCommand>
    {
        private readonly IUserService loginService;
        public ActivateUserHandler(IUserService loginService)
        {
            this.loginService = loginService;
        }

        public async Task<ICommandResult> HandleAsync(ActivateUserCommand command)
        {
            var result = await this.loginService.ActivateUserAsync(command);

            return new CommandResult { IsSuccess = result };
        }
    }
}