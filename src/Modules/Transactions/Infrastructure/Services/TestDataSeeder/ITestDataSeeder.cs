using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BudgetUnderControl.Modules.Transactions.Infrastructure.Services
{
    public interface ITestDataSeeder
    {
        Task SeedAsync();
    }
}
