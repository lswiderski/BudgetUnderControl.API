using BudgetUnderControl.Domain;
using BudgetUnderControl.Domain.Repositiories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetUnderControl.Infrastructure
{
    public class AccountGroupRepository : BaseModel, IAccountGroupRepository
    {
        public AccountGroupRepository(IContextFacade context) : base(context)
        {
        }

        public async Task<ICollection<AccountGroup>> GetAccountGroupsAsync()
        {
            var list = await (from ag in this.Context.AccountGroup
                              select ag
                        ).ToListAsync();

            return list;
        }

        public async Task<AccountGroup> GetAccountGroupAsync(Guid id)
        {
            var accountGroup = await (from ag in this.Context.AccountGroup
                                      select ag
                        ).FirstOrDefaultAsync(x => x.ExternalId == id);

            return accountGroup;
        }

        public async Task UpdateAsync(AccountGroup accountGroup)
        {
            accountGroup.UpdateModify();
            this.Context.AccountGroup.Update(accountGroup);
            await this.Context.SaveChangesAsync();
        }

        public async Task AddAccountGroupAsync(AccountGroup accountGroup)
        {
            this.Context.AccountGroup.Add(accountGroup);
            await this.Context.SaveChangesAsync();
        }
    }
}
