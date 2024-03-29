﻿using BudgetUnderControl.Modules.Transactions.Application.DTO;
using BudgetUnderControl.Shared.Application.CQRS.Configuration.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetUnderControl.Modules.Transactions.Application.Commands.Categories.GetCategory
{
    public class GetCategoryQuery : QueryBase<CategoryListItemDTO>
    {
        public GetCategoryQuery(Guid categoryId)
        {
            CategoryId = categoryId;
        }

        public Guid CategoryId { get; }
    }
}

