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

        Task ValidateLoginAsync(MobileLoginCommand command);

        Task RegisterUserAsync(RegisterUserCommand command);

        Task<bool> ActivateUserAsync(ActivateUserCommand command);

        Task ResetActivationCodeAsync(Guid userId);

        Task EditUserAsync(EditUser command);
    }
}
