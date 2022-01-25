
using BudgetUnderControl.Shared.Application.CQRS.Contracts;

namespace BudgetUnderControl.Modules.Users.Application.Commands.Login.CreateNewUser
{
    public class CreateNewUserCommand : CommandBase<string>
    {
        public CreateNewUserCommand(string username, string firstname, string lastname, string email, string password)
        {
            Username = username;
            FirstName = firstname;
            LastName = lastname;
            Email = email;
            Password = password;
        }

        public CreateNewUserCommand()
        {

        }
        public string Username { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }
    }
}
