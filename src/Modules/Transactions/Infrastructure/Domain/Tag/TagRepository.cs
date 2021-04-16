using BudgetUnderControl.Modules.Transactions.Application.Services;
using BudgetUnderControl.Domain;
using BudgetUnderControl.Domain.Repositiories;
using BudgetUnderControl.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BudgetUnderControl.Shared.Abstractions.Contexts;

namespace BudgetUnderControl.Infrastructure.Repositories
{
    public class TagRepository : ITagRepository
    {
        private readonly IContext context;
        private readonly TransactionsContext transactionsContext;

        public TagRepository(TransactionsContext transactionsContext, IContext context) 
        {
            this.context = context;
            this.transactionsContext = transactionsContext;
        }

        public async Task AddAsync(Tag tag)
        {
            await this.transactionsContext.Tags.AddAsync(tag);
            await this.transactionsContext.SaveChangesAsync();
        }

        public async Task<ICollection<Tag>> GetAsync()
        {
            var list = await this.transactionsContext.Tags
                .Where(t => t.OwnerId == context.Identity.ObsoleteUserId)
                .OrderByDescending(t => t.ModifiedOn)
                .ToListAsync();

            return list;
        }

        public async Task<ICollection<Tag>> GetAsync(List<int> tagIds)
        {
            var list = await this.transactionsContext.Tags
                .Where(t => t.OwnerId == context.Identity.ObsoleteUserId
                && tagIds.Contains(t.Id))
                .OrderByDescending(t => t.ModifiedOn)
                .ToListAsync();

            return list;
        }

        public async Task<ICollection<Tag>> GetAsync(List<Guid> tagIds)
        {
            var list = await this.transactionsContext.Tags
                .Where(t => t.OwnerId == context.Identity.ObsoleteUserId
                && tagIds.Contains(t.ExternalId))
                .OrderByDescending(t => t.ModifiedOn)
                .ToListAsync();

            return list;
        }

        public async Task<Tag> GetAsync(int id)
        {
            var tag = await this.transactionsContext.Tags
                .Where(t => t.OwnerId == context.Identity.ObsoleteUserId)
                .FirstOrDefaultAsync(x => x.Id == id);

            return tag;
        }

        public async Task<Tag> GetAsync(Guid id)
        {
            var tag = await this.transactionsContext.Tags.Where(t => t.OwnerId == context.Identity.ObsoleteUserId).FirstOrDefaultAsync(x => x.ExternalId == id);

            return tag;
        }

        public async Task UpdateAsync(Tag tag)
        {
            this.transactionsContext.Tags.Update(tag);
            await this.transactionsContext.SaveChangesAsync();
        }

        public async Task RemoveAsync(Tag tag)
        {
            this.transactionsContext.Tags.Remove(tag);
            await this.transactionsContext.SaveChangesAsync();
        }

        public async Task RemoveAsync(IEnumerable<Tag> tags)
        {
            var tagIds = tags.Select(x => x.Id).ToList();
            var t2t = await this.transactionsContext.TagsToTransactions.Where(t => tagIds.Contains(t.TagId)).ToListAsync();
            await this.RemoveAsync(t2t);
            this.transactionsContext.Tags.RemoveRange(tags);
            await this.transactionsContext.SaveChangesAsync();
        }

        public async Task<TagToTransaction> GetTagToTransactionAsync(int tagToTransactionId)
        {
            var tagToTransaction = await this.transactionsContext.TagsToTransactions.FirstOrDefaultAsync(x => x.Id == tagToTransactionId);
            return tagToTransaction;
        }

        public async Task<ICollection<TagToTransaction>> GetTagToTransactionsAsync()
        {
            var list = await this.transactionsContext.TagsToTransactions.ToListAsync();
            return list;
        }

        public async Task<ICollection<TagToTransaction>> GetTagToTransactionsAsync(int transactionId)
        {
            var list = await this.transactionsContext.TagsToTransactions
                .Where(t => t.TransactionId == transactionId)
                .ToListAsync();
            return list;
        }

        public async Task AddAsync(TagToTransaction tagToTransaction)
        {
            await this.transactionsContext.TagsToTransactions.AddAsync(tagToTransaction);
            await this.transactionsContext.SaveChangesAsync();
        }

        public async Task AddAsync(IEnumerable<TagToTransaction> tagsToTransaction)
        {
            await this.transactionsContext.TagsToTransactions.AddRangeAsync(tagsToTransaction);
            await this.transactionsContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(TagToTransaction tagToTransaction)
        {
            this.transactionsContext.TagsToTransactions.Update(tagToTransaction);
            await this.transactionsContext.SaveChangesAsync();
        }

        public async Task RemoveAsync(TagToTransaction tagToTransaction)
        {
            this.transactionsContext.TagsToTransactions.Remove(tagToTransaction);
            await this.transactionsContext.SaveChangesAsync();
        }

        public async Task RemoveAsync(IEnumerable<TagToTransaction> tagsToTransaction)
        {
            this.transactionsContext.TagsToTransactions.RemoveRange(tagsToTransaction);
            await this.transactionsContext.SaveChangesAsync();
        }
    }
}
