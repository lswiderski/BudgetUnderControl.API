using BudgetUnderControl.Modules.Transactions.Application.DTO;
using BudgetUnderControl.Modules.Transactions.Application.Commands.Tags.CreateTag;
using BudgetUnderControl.Modules.Transactions.Application.Commands.Tags.UpdateTag;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BudgetUnderControl.Modules.Transactions.Application.Services
{
    public interface ITagService
    {
        Task<ICollection<TagDTO>> GetTagsAsync();
        Task<ICollection<TagDTO>> GetActiveTagsAsync();
        Task<TagDTO> GetTagAsync(Guid tagId);
        Task AddTagAsync(CreateTagCommand command);
        Task EditTagAsync(UpdateTagCommand command);
    }
}
