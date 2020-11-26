using System.Threading.Tasks;

namespace BudgetUnderControl.CommonInfrastructure.Commands
{
    public class AddTransactionHandler : ICommandHandler<AddTransaction>
    {
        private readonly ITransactionService transactionService;
        public AddTransactionHandler(ITransactionService transactionService)
        {
            this.transactionService = transactionService;
        }

        public async Task HandleAsync(AddTransaction command)
        {
            await this.transactionService.AddTransactionAsync(command);
            
        }
    }
}
