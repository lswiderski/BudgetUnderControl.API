using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BudgetUnderControl.Domain.Repositiories
{
    public interface ITagRepository
    {
        Task AddAsync(Tag tag);
        Task<ICollection<Tag>> GetAsync();
        Task<ICollection<Tag>> GetAsync(List<int> tagIds);
        Task<ICollection<Tag>> GetAsync(List<Guid> tagIds);
        Task<Tag> GetAsync(int id);
        Task<Tag> GetAsync(Guid id);
        Task UpdateAsync(Tag tag);
        Task RemoveAsync(Tag tag);
        Task RemoveAsync(IEnumerable<Tag> tags);

        Task AddAsync(TagToTransaction tagToTransaction);
        Task AddAsync(IEnumerable<TagToTransaction> tagsToTransaction);
        Task<ICollection<TagToTransaction>> GetTagToTransactionsAsync();
        Task<ICollection<TagToTransaction>> GetTagToTransactionsAsync(int transactionId);
        Task<TagToTransaction> GetTagToTransactionAsync(int tagToTransactionId);        
        Task UpdateAsync(TagToTransaction tagToTransaction);
        Task RemoveAsync(TagToTransaction tagToTransaction);
        Task RemoveAsync(IEnumerable<TagToTransaction> tagsToTransaction);
    }
}
