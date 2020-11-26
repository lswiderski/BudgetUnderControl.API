using System.Threading.Tasks;

namespace BudgetUnderControl.CommonInfrastructure.Commands.Transaction
{
    public class DeleteTransactionHandler : ICommandHandler<DeleteTransaction>
    {
        private readonly ITransactionService transactionService;
        public DeleteTransactionHandler(ITransactionService transactionService)
        {
            this.transactionService = transactionService;
        }

        public async Task HandleAsync(DeleteTransaction command)
        {
            await this.transactionService.DeleteTransactionAsync(command);
        }
    }
}
