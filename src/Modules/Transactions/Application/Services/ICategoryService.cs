using BudgetUnderControl.Modules.Transactions.Application.DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BudgetUnderControl.Modules.Transactions.Application.Services
{
    public interface ICategoryService
    {
        Task<ICollection<CategoryListItemDTO>> GetCategoriesAsync();
        Task<CategoryListItemDTO> GetCategoryAsync(Guid id);
        Task<bool> IsValidAsync(int categoryId);
    }
}
