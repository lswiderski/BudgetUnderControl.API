using BudgetUnderControl.Shared.Application.CQRS.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetUnderControl.Modules.Users.Application.Commands.Users.ResetUserActivation
{
    public class ResetUserActivationCommand : CommandBase
    {
        public ResetUserActivationCommand(Guid? userId = null)
        {
            UserId = userId;
        }

        public Guid? UserId { get; }
    }
}
