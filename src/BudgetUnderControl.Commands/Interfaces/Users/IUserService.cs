using BudgetUnderControl.CommonInfrastructure.Commands;
using BudgetUnderControl.CommonInfrastructure.Commands.User;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BudgetUnderControl.CommonInfrastructure
{
    public interface IUserService
    {
        IUserIdentityContext CreateUserIdentityContext(string userId);

        Task<string> ValidateLoginAsync(MobileLoginCommand command);

        Task<string> RegisterUserAsync(RegisterUserCommand command);

        Task<bool> ActivateUserAsync(ActivateUserCommand command);

        Task ResetActivationCodeAsync(Guid userId);

        Task EditUserAsync(EditUser command);
    }
}
