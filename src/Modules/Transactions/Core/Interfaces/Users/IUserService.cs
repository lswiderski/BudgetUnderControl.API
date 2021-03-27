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

        Task<string> ValidateLoginAsync(string username, string password);

        Task<string> RegisterUserAsync(RegisterUserCommand command);

        Task<bool> ActivateUserAsync(Guid userId, string code);

        Task ResetActivationCodeAsync(Guid userId);

        Task EditUserAsync(EditUser command);
    }
}
