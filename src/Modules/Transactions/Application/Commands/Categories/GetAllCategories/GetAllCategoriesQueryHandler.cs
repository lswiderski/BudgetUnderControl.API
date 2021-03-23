using BudgetUnderControl.Common.Contracts;
using BudgetUnderControl.CommonInfrastructure;
using BudgetUnderControl.Modules.Transactions.Application.Configuration.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BudgetUnderControl.Modules.Transactions.Application.Commands.Categories.GetAllCategories
{
    internal class GetAllCategoriesQueryHandler : IQueryHandler<GetAllCategoriesQuery, List<CategoryListItemDTO>>
    {
        private readonly ICategoryService categoryService;

        public GetAllCategoriesQueryHandler(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }

        public async Task<List<CategoryListItemDTO>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
        {
            var categories = await this.categoryService.GetCategoriesAsync();
            return categories.ToList();
        }
    }
}