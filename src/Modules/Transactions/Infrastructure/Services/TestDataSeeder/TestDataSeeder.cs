using BudgetUnderControl.Common.Enums;
using BudgetUnderControl.Modules.Transactions.Application.Services;
using BudgetUnderControl.Modules.Transactions.Application.Commands.Accounts.CreateAccount;
using BudgetUnderControl.Modules.Transactions.Application.DTO;
using BudgetUnderControl.Modules.Transactions.Application.Transactions.AddTransaction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetUnderControl.Modules.Transactions.Infrastructure.Services
{
    public class TestDataSeeder : ITestDataSeeder
    {
        private readonly IAccountService accountService;
        private readonly IAccountGroupService accountGroupService;
        private readonly ITransactionService transactionService;
        private readonly ICurrencyService currencyService;
        private readonly ICategoryService categoryService;

        public TestDataSeeder(IAccountService accountService, ITransactionService transactionService,
            IAccountGroupService accountGroupService, ICurrencyService currencyService, ICategoryService categoryService)
        {
            this.accountService = accountService;
            this.transactionService = transactionService;
            this.accountGroupService = accountGroupService;
            this.currencyService = currencyService;
            this.categoryService = categoryService;
        }

        public async Task SeedAsync()
        {
            var accountGroup = (await this.accountGroupService.GetAccountGroupsAsync()).First();
            var currencies = await this.currencyService.GetCurriencesAsync();
            var addAccountCommand = new CreateAccountCommand
            {
                AccountGroupId = accountGroup.Id,
                Amount = 0,
                CurrencyId = currencies.First().Id,
                IsIncludedInTotal = true,
                Name = "Test Account",
                Order = 0,
                Type = AccountType.Account
            };
           

            var account = (await accountService.GetAccountsWithBalanceAsync(true)).First();
            var categories = await categoryService.GetCategoriesAsync();
            Random random = new Random();
            for (int i = 0; i < 5; i++)
            {
                var amount = random.Next(-20, 20);
                var addTransactionCommand = new AddTransactionCommand
                {
                    AccountId = account.Id,
                    Amount = amount,
                    Date = DateTime.UtcNow,
                    Type = amount < 0 ? ExtendedTransactionType.Expense : ExtendedTransactionType.Income,
                    CategoryId = categories.First().Id,
                    Name = Guid.NewGuid().ToString()
                };
            }
        }
    }
}
