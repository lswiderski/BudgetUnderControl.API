using BudgetUnderControl.Modules.Transactions.Application.DTO;
using BudgetUnderControl.Modules.Transactions.Application.Services;
using BudgetUnderControl.Modules.Transactions.Application.Configuration.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BudgetUnderControl.Modules.Transactions.Application.Commands.Categories.GetCategory
{
    internal class GetCategoryQueryHandler : IQueryHandler<GetCategoryQuery, CategoryListItemDTO>
    {
        private readonly ICategoryService categoryService;

        public GetCategoryQueryHandler(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }
        public async Task<CategoryListItemDTO> Handle(GetCategoryQuery request, CancellationToken cancellationToken)
        {
            var currency = await this.categoryService.GetCategoryAsync(request.CategoryId);
            return currency;
        }
    }
}