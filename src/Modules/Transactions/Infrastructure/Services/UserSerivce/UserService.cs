using System;
using System.Threading.Tasks;
using BudgetUnderControl.Modules.Transactions.Application.Services;

namespace BudgetUnderControl.Modules.Transactions.Infrastructure.Services.UserSerivce
{
    public class UserService : IUserService
    {
        private readonly ICategoryService categoryService;

        public UserService(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }

        public async Task SeedNewUserAsync(Guid userId)
        {
            await this.categoryService.SeedCategoriesAsync(userId);
        }
    }
}