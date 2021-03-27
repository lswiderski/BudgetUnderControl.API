using BudgetUnderControl.CommonInfrastructure;
using BudgetUnderControl.Domain;
using BudgetUnderControl.Domain.Repositiories;
using BudgetUnderControl.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetUnderControl.Infrastructure.Repositories
{
    public class TagRepository : BaseModel, ITagRepository
    {
        private readonly IUserIdentityContext userIdentityContext;

        public TagRepository(IContextFacade context, IUserIdentityContext userIdentityContext) : base(context)
        {
            this.userIdentityContext = userIdentityContext;
        }

        public async Task AddAsync(Tag tag)
        {
            await this.Context.Tags.AddAsync(tag);
            await this.Context.SaveChangesAsync();
        }

        public async Task<ICollection<Tag>> GetAsync()
        {
            var list = await this.Context.Tags
                .Where(t => t.OwnerId == userIdentityContext.UserId)
                .OrderByDescending(t => t.ModifiedOn)
                .ToListAsync();

            return list;
        }

        public async Task<ICollection<Tag>> GetAsync(List<int> tagIds)
        {
            var list = await this.Context.Tags
                .Where(t => t.OwnerId == userIdentityContext.UserId
                && tagIds.Contains(t.Id))
                .OrderByDescending(t => t.ModifiedOn)
                .ToListAsync();

            return list;
        }

        public async Task<ICollection<Tag>> GetAsync(List<Guid> tagIds)
        {
            var list = await this.Context.Tags
                .Where(t => t.OwnerId == userIdentityContext.UserId
                && tagIds.Contains(t.ExternalId))
                .OrderByDescending(t => t.ModifiedOn)
                .ToListAsync();

            return list;
        }

        public async Task<Tag> GetAsync(int id)
        {
            var tag = await this.Context.Tags
                .Where(t => t.OwnerId == userIdentityContext.UserId)
                .FirstOrDefaultAsync(x => x.Id == id);

            return tag;
        }

        public async Task<Tag> GetAsync(Guid id)
        {
            var tag = await this.Context.Tags.Where(t => t.OwnerId == userIdentityContext.UserId).FirstOrDefaultAsync(x => x.ExternalId == id);

            return tag;
        }

        public async Task UpdateAsync(Tag tag)
        {
            this.Context.Tags.Update(tag);
            await this.Context.SaveChangesAsync();
        }

        public async Task RemoveAsync(Tag tag)
        {
            this.Context.Tags.Remove(tag);
            await this.Context.SaveChangesAsync();
        }

        public async Task RemoveAsync(IEnumerable<Tag> tags)
        {
            var tagIds = tags.Select(x => x.Id).ToList();
            var t2t = await this.Context.TagsToTransactions.Where(t => tagIds.Contains(t.TagId)).ToListAsync();
            await this.RemoveAsync(t2t);
            this.Context.Tags.RemoveRange(tags);
            await this.Context.SaveChangesAsync();
        }

        public async Task<TagToTransaction> GetTagToTransactionAsync(int tagToTransactionId)
        {
            var tagToTransaction = await this.Context.TagsToTransactions.FirstOrDefaultAsync(x => x.Id == tagToTransactionId);
            return tagToTransaction;
        }

        public async Task<ICollection<TagToTransaction>> GetTagToTransactionsAsync()
        {
            var list = await this.Context.TagsToTransactions.ToListAsync();
            return list;
        }

        public async Task<ICollection<TagToTransaction>> GetTagToTransactionsAsync(int transactionId)
        {
            var list = await this.Context.TagsToTransactions
                .Where(t => t.TransactionId == transactionId)
                .ToListAsync();
            return list;
        }

        public async Task AddAsync(TagToTransaction tagToTransaction)
        {
            await this.Context.TagsToTransactions.AddAsync(tagToTransaction);
            await this.Context.SaveChangesAsync();
        }

        public async Task AddAsync(IEnumerable<TagToTransaction> tagsToTransaction)
        {
            await this.Context.TagsToTransactions.AddRangeAsync(tagsToTransaction);
            await this.Context.SaveChangesAsync();
        }

        public async Task UpdateAsync(TagToTransaction tagToTransaction)
        {
            this.Context.TagsToTransactions.Update(tagToTransaction);
            await this.Context.SaveChangesAsync();
        }

        public async Task RemoveAsync(TagToTransaction tagToTransaction)
        {
            this.Context.TagsToTransactions.Remove(tagToTransaction);
            await this.Context.SaveChangesAsync();
        }

        public async Task RemoveAsync(IEnumerable<TagToTransaction> tagsToTransaction)
        {
            this.Context.TagsToTransactions.RemoveRange(tagsToTransaction);
            await this.Context.SaveChangesAsync();
        }
    }
}
