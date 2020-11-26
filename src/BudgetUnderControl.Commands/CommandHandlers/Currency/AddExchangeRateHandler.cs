using System.Threading.Tasks;

namespace BudgetUnderControl.CommonInfrastructure.Commands
{
    public class AddExchangeRateHandler : ICommandHandler<AddExchangeRate>
    {
        private readonly ICurrencyService currencyService;
        public AddExchangeRateHandler(ICurrencyService currencyService)
        {
            this.currencyService = currencyService;
        }

        public async Task HandleAsync(AddExchangeRate command)
        {
            await this.currencyService.AddExchangeRateAsync(command);
        }
    }
}
