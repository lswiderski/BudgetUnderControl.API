using System.Threading.Tasks;

namespace BudgetUnderControl.CommonInfrastructure.Commands
{
    public class DeleteAccountHandler : ICommandHandler<DeleteAccount>
    {
        private readonly IAccountService accountService;
        public DeleteAccountHandler(IAccountService accountService)
        {
            this.accountService = accountService;
        }

        public async Task HandleAsync(DeleteAccount command)
        {
            await this.accountService.DeleteAccountAsync(command);

        }
    }
}
