using BudgetUnderControl.Modules.Transactions.Application.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetUnderControl.Modules.Transactions.Application.Commands.Users.ActivateUser
{
    public class ActivateUserCommand : CommandBase<bool>
    {
        public ActivateUserCommand(string code)
        {
            Code = code;
        }

        public ActivateUserCommand()
        {

        }

        public string Code { get; set; }
    }
}
