using System;
using System.Collections.Generic;
using System.Text;

namespace BudgetUnderControl.CommonInfrastructure.Commands
{
    public class ActivateUserCommand : ICommand
    {
        public string Email { get; set; }

        public string Username { get; set; }

        public string Code { get; set; }
    }
}
