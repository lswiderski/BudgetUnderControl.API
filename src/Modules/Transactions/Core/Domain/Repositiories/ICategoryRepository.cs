using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BudgetUnderControl.Domain.Repositiories
{
    public interface ICategoryRepository
    {
        Task<ICollection<Category>> GetCategoriesAsync();
        Task<ICollection<Category>> GetAllCategoriesAsync();
        Task<Category> GetCategoryAsync(Guid id);

        Task<Category> GetCategoryAsync(string name);
        Task UpdateAsync(Category category);
        Task AddCategoryAsync(Category category);

        Task AddCategoriesAsync(IEnumerable<Category> categories);

        Task HardRemoveCategoriesAsync(IEnumerable<Category> categories);
    }
}
