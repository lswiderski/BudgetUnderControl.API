using BudgetUnderControl.Domain;

namespace BudgetUnderControl.Modules.Transactions.Application.Services
{
    public interface IJwtHandlerService
    {
        string CreateToken(User user);
    }
}
