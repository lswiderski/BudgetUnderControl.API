using BudgetUnderControl.Modules.Transactions.Application.DTO;
using BudgetUnderControl.Domain.Repositiories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BudgetUnderControl.Modules.Transactions.Application.Services;

namespace BudgetUnderControl.Modules.Transactions.Infrastructure.Services
{
    public class AccountGroupService : IAccountGroupService
    {

        private readonly IAccountGroupRepository accountGroupRepository;

        public AccountGroupService(IAccountGroupRepository accountGroupRepository)
        {
            this.accountGroupRepository = accountGroupRepository;
        }

        public async Task<ICollection<AccountGroupItemDTO>> GetAccountGroupsAsync()
        {
            var groups = await this.accountGroupRepository.GetAccountGroupsAsync();
            var list = (from ag in groups
                        select new AccountGroupItemDTO
                        {
                            Id = ag.Id,
                            Name = ag.Name,
                            ExternalId = ag.ExternalId,
                        }
                        ).ToList();

            return list;
        }

        public async Task<AccountGroupItemDTO> GetAccountGroupAsync(Guid id)
        {
            var group = await this.accountGroupRepository.GetAccountGroupAsync(id);
            var dto = new AccountGroupItemDTO
            {
                Id = group.Id,
                ExternalId = group.ExternalId,
                Name = group.Name,
            };

            return dto;
        }

        public async Task<bool> IsValidAsync(int id)
        {
            var accountsGroups = await this.accountGroupRepository.GetAccountGroupsAsync();
            return accountsGroups.Any(x => x.Id == id);
        }
    }
}
