using BudgetUnderControl.Common.Enums;
using BudgetUnderControl.Modules.Transactions.Application.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetUnderControl.Modules.Transactions.Application.Commands.Login.CreateNewUser
{
    public class CreateNewUserCommand : CommandBase<string>
    {
        public CreateNewUserCommand(string username, string firstname, string lastname, string email, string password, Guid tokenId)
        {
            Username = username;
            FirstName = firstname;
            LastName = lastname;
            Email = email;
            Password = password;
            TokenId = tokenId;
        }

        public CreateNewUserCommand()
        {

        }
        public string Username { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public Guid TokenId { get; set; }
    }
}
