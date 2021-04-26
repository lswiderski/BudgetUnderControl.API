using BudgetUnderControl.Shared.Application.CQRS.Contracts;

namespace BudgetUnderControl.Modules.Users.Application.Commands.Login.AuthenticateAdmin
{
    public class AuthenticateAdminCommand : CommandBase<string>
    {
        public AuthenticateAdminCommand(string username, string password)
        {
            Username = username;
            Password = password;
        }

        public AuthenticateAdminCommand()
        {

        }

        public string Username { get; set; }

        public string Password { get; set; }
    }
}
