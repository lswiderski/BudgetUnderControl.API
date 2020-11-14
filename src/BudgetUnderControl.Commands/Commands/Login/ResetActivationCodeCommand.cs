using System;
using System.Collections.Generic;
using System.Text;

namespace BudgetUnderControl.CommonInfrastructure.Commands.Login
{
    public class ResetActivationCodeCommand : ICommand
    {
        public Guid UserId { get; set; }
    }
}
