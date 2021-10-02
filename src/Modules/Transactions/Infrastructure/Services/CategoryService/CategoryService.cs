using BudgetUnderControl.Modules.Transactions.Application.DTO;
using BudgetUnderControl.Domain.Repositiories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BudgetUnderControl.Domain;
using BudgetUnderControl.Modules.Transactions.Application.Services;
using BudgetUnderControl.Modules.Transactions.Application.Services;
using BudgetUnderControl.Shared.Abstractions.Contexts;

namespace BudgetUnderControl.Modules.Transactions.Infrastructure.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository categoryRepository;
        private readonly IContext context;

        public CategoryService(ICategoryRepository categoryRepository, IContext context)
        {
            this.categoryRepository = categoryRepository;
            this.context = context;
        }

        public async Task<ICollection<CategoryListItemDTO>> GetCategoriesAsync()
        {
            var categories = await this.categoryRepository.GetCategoriesAsync();

            // Temporary solutions
            // TODO move it to queue on user creation
            if (!categories.Any())
            {
                await this.SeedCategoriesAsync(this.context.Identity.Id);
                categories = await this.categoryRepository.GetCategoriesAsync();
            }
            
            var dtos = categories.Select(x => new CategoryListItemDTO
            {
                Id = x.Id,
                Name = x.Name,
                ExternalId = x.ExternalId,
                
            })
               .ToList();

           

            return dtos;
        }

        public async Task<CategoryListItemDTO> GetCategoryAsync(Guid id)
        {
            var category = await this.categoryRepository.GetCategoryAsync(id);

            var dto = new CategoryListItemDTO
            {
                Id = category.Id,
                Name = category.Name,
                ExternalId = category.ExternalId
            };

            return dto;
        }

        public async Task<bool> IsValidAsync(int categoryId)
        {
            var currencies = await this.categoryRepository.GetCategoriesAsync();
            return currencies.Any(x => x.Id == categoryId);
        }

        public async Task<CategoryListItemDTO> GetDefaultCategoryAsync()
        {
            var category = (await this.categoryRepository.GetCategoriesAsync())
                .FirstOrDefault(x => x.IsDefault && !x.IsDeleted);
            if (category != null)
            {
                var dto = new CategoryListItemDTO
                {
                    Id = category.Id,
                    Name = category.Name,
                    ExternalId = category.ExternalId,
                    Icon = category.Icon
                };
                return dto;
            }

            return null;
        }
        
        public async Task SeedCategoriesAsync(Guid userId)
        {
            var categories = new List<Category>()
            {
                Category.Create("Food", userId).SetIcon("Utensils.FAS"),
                Category.Create("Transport", userId).SetIcon("Bus.FAS"),
                Category.Create("Other", userId).SetDefault().SetIcon("StickyNote.FAR"),
                Category.Create("Salary", userId).SetIcon("HandHoldingUsd.FAS"),
                Category.Create("Taxes", userId).SetIcon("Receipt.FAS"),             
                Category.Create("Entertainment", userId).SetIcon("GlassCheers.FAS"),
                Category.Create("Health", userId).SetIcon("Heartbeat.FAS"),
                Category.Create("Interest", userId).SetIcon("MapMarkerAlt.FAS"),
                Category.Create("Home", userId).SetIcon("Home.FAS"),
                Category.Create("Beauty", userId).SetIcon("Smile.FAR"),
                Category.Create("Groceries", userId).SetIcon("ShoppingBasket.FAS"),
                Category.Create("Loans", userId).SetIcon("MoneyCheckAlt.FAS"),
                Category.Create("Gift", userId).SetIcon("Gift.FAS"),
                Category.Create("Accommodation", userId).SetIcon("Bed.FAS"),
                Category.Create("Sightseeing", userId).SetIcon("Landmark.FAS"),
                Category.Create("Shopping", userId).SetIcon("Tshirt.FAS"),
                Category.Create("Professional Costs", userId).SetIcon("Suitcase.FAS"),
            };
            await categoryRepository.AddCategoriesAsync(categories);
        }
        
    }
}
