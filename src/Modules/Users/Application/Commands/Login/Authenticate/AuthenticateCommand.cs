using BudgetUnderControl.Shared.Application.CQRS.Contracts;

namespace BudgetUnderControl.Modules.Users.Application.Commands.Login.Authenticate
{
    public class AuthenticateCommand : CommandBase<string>
    {
        public AuthenticateCommand(string username, string password)
        {
            Username = username;
            Password = password;
        }

        public AuthenticateCommand()
        {

        }

        public string Username { get; set; }

        public string Password { get; set; }
    }
}
