using BudgetUnderControl.Shared.Application.CQRS.Contracts;

namespace BudgetUnderControl.Modules.Users.Application.Commands.Users.ActivateUser
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
