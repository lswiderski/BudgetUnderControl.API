using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetUnderControl.Domain.Repositiories
{
    public interface IAccountGroupRepository
    {
        Task<ICollection<AccountGroup>> GetAccountGroupsAsync();
        Task<AccountGroup> GetAccountGroupAsync(Guid id);
        Task UpdateAsync(AccountGroup accountGroup);
        Task AddAccountGroupAsync(AccountGroup accountGroup);
    }
}
