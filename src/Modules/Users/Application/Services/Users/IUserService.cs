using BudgetUnderControl.Modules.Users.Application.Commands.Login.CreateNewUser;
using BudgetUnderControl.Modules.Users.Application.Commands.Users.UpdateUser;
using System;
using System.Threading.Tasks;

namespace BudgetUnderControl.Modules.Users.Application.Services
{
    public interface IUserService
    {
        IUserIdentityContext CreateUserIdentityContext(string userId);

        Task<string> ValidateLoginAsync(string username, string password);

        Task<string> RegisterUserAsync(CreateNewUserCommand command);

        Task<bool> ActivateUserAsync(Guid userId, string code);

        Task ResetActivationCodeAsync(Guid userId);

        Task EditUserAsync(UpdateUserCommand command);
    }
}
