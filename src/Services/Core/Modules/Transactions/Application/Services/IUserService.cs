using System;
using System.Threading.Tasks;

namespace BudgetUnderControl.Modules.Transactions.Application.Services
{
    public interface IUserService
    {
        Task SeedNewUserAsync(Guid userId);
    }
}