using BudgetUnderControl.Common.Contracts;
using BudgetUnderControl.Modules.Transactions.Application.Commands.Categories.GetAllCategories;
using BudgetUnderControl.Modules.Transactions.Application.Commands.Categories.GetCategory;
using BudgetUnderControl.Modules.Transactions.Application.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BudgetUnderControl.Modules.Transactions.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ITransactionsModule _transactionsModule;

        public CategoriesController(ITransactionsModule transactionsModule)
        {
            _transactionsModule = transactionsModule;
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<CategoryListItemDTO>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Get()
        {
            var categories = await this._transactionsModule.ExecuteQueryAsync(new GetAllCategoriesQuery());
            return Ok(categories);
        }

        // GET api/currencies/1
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(CategoryListItemDTO), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetById(Guid id)
        {
            var category = await this._transactionsModule.ExecuteQueryAsync(new GetCategoryQuery(id));
            return Ok(category);
        }
    }
}

