using BudgetUnderControl.CommonInfrastructure.Commands;
using BudgetUnderControl.CommonInfrastructure.Commands.User;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BudgetUnderControl.CommonInfrastructure.CommandHandlers.User
{
    public class EditUserHandler : ICommandHandler<EditUser>
    {
        private readonly IUserService userService;
        public EditUserHandler(IUserService userService)
        {
            this.userService = userService;
        }

        public async Task HandleAsync(EditUser command)
        {
            await this.userService.EditUserAsync(command);
        }
    }
}
