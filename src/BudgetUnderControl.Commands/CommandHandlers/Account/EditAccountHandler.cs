using System.Threading.Tasks;

namespace BudgetUnderControl.CommonInfrastructure.Commands
{
    public class EditAccountHandler : ICommandHandler<EditAccount>
    {
        private readonly IAccountService accountService;
        public EditAccountHandler(IAccountService accountService)
        {
            this.accountService = accountService;
        }

        public async Task HandleAsync(EditAccount command)
        {
            await this.accountService.EditAccountAsync(command);

        }
    }
}
