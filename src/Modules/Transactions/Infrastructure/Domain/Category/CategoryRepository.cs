using BudgetUnderControl.Modules.Transactions.Application.DTO;
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

namespace BudgetUnderControl.Infrastructure
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly IContext userIdentityContext;
        private readonly TransactionsContext Context;

        public CategoryRepository(TransactionsContext context, IContext userIdentityContext) 
        {
            this.userIdentityContext = userIdentityContext;
            this.Context = context;
        }

        public async Task<ICollection<Category>> GetCategoriesAsync()
        {
            var list = await this.Context.Categories/*Where(x => x.OwnerId == userIdentityContext.Identity.Id)*/.ToListAsync();
            return list;
        }

        public async Task<ICollection<Category>> GetAllCategoriesAsync()
        {
            var list = await this.Context.Categories.ToListAsync();
            return list;
        }

        public async Task<Category> GetCategoryAsync(Guid id)
        {
            var category = await this.Context.Categories.Where(x => x.ExternalId == id).FirstOrDefaultAsync();
            return category;
        }

        public async Task<Category> GetCategoryAsync(string name)
        {
            var category = await this.Context.Categories.Where(x => x.Name == name).FirstOrDefaultAsync();
            return category;
        }

        public async Task UpdateAsync(Category category)
        {
            category.UpdateModify();
            this.Context.Categories.Update(category);
            await this.Context.SaveChangesAsync();
        }

        public async Task AddCategoryAsync(Category category)
        {
            this.Context.Categories.Add(category);
            await this.Context.SaveChangesAsync();
        }
    }
}
