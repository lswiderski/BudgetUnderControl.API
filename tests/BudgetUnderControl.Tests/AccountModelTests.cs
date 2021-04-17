
using Microsoft.EntityFrameworkCore;
using Moq;
using NLog;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace BudgetUnderControl.Tests
{
    public class AccountModelTests
    {
        [Fact]
        public async Task activate_account_async_should_active_selected_account()
        {
           /* var accountRepositoryMock = new Mock<IAccountRepository>();
            var userRepositoryMock = new Mock<IUserRepository>();
            var loggerMock = new Mock<ILogger>();
            var accountService = new AccountService(accountRepositoryMock.Object, userRepositoryMock.Object, loggerMock.Object);

            var account = Account.Create("test", 1, 1, true, "", 1, Common.Enums.AccountType.Wallet, null, true, 1);
            account.Currency = Currency.Create( "PLN", "Polski zloty", 985, "zl");
           
            accountRepositoryMock.Setup(x => x.GetAccountAsync(It.IsAny<Guid>())).ReturnsAsync(account);
            accountRepositoryMock.Setup(x => x.GetAccountAsync(It.IsAny<int>())).ReturnsAsync(account);
            accountRepositoryMock.Setup(x => x.GetActualBalanceAsync(It.IsAny<int>())).ReturnsAsync(10);
            await accountService.ActivateAccountAsync(account.Id);

            var accountDTO = await accountService.GetAccountAsync(account.ExternalId);
            Assert.True(accountDTO.IsActive);
*/
        }
    }
}
