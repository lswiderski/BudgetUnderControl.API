using BudgetUnderControl.Modules.Transactions.Application.DTO;
using BudgetUnderControl.Modules.Transactions.Application.Services;
using BudgetUnderControl.Modules.Transactions.Application.Configuration.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BudgetUnderControl.Modules.Transactions.Application.Commands.Tags.GetTag
{
    internal class GetTagQueryHandler : IQueryHandler<GetTagQuery, TagDTO>
    {
        private readonly ITagService tagService;

        public GetTagQueryHandler(ITagService tagService)
        {
            this.tagService = tagService;
        }
        public async Task<TagDTO> Handle(GetTagQuery request, CancellationToken cancellationToken)
        {
            var tag = await this.tagService.GetTagAsync(request.TagId);
            return tag;
        }
    }
}