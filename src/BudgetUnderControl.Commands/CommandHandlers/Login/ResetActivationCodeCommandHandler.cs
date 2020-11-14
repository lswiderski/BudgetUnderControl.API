using BudgetUnderControl.CommonInfrastructure.Commands;
using BudgetUnderControl.CommonInfrastructure.Commands.Login;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BudgetUnderControl.CommonInfrastructure.CommandHandlers
{
    public class ResetActivationCodeCommandHandler : ICommandHandler<ResetActivationCodeCommand>
    {
        private readonly IUserService loginService;
        public ResetActivationCodeCommandHandler(IUserService loginService)
        {
            this.loginService = loginService;
        }

        public async Task HandleAsync(ResetActivationCodeCommand command)
        {
             await this.loginService.ResetActivationCodeAsync(command.UserId);

        }
    }
}