using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BudgetUnderControl.Infrastructure.Services
{
    public interface ITestDataSeeder
    {
        Task SeedAsync();
    }
}
