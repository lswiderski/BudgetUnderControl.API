using BudgetUnderControl.Shared.Application.CQRS.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetUnderControl.Modules.Transactions.Application.Commands.Login.Authenticate
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
