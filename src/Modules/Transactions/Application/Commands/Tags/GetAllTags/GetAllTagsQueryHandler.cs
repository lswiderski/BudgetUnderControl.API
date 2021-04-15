using BudgetUnderControl.Modules.Transactions.Application.DTO;
using BudgetUnderControl.Modules.Transactions.Application.Services;
using BudgetUnderControl.Shared.Application.CQRS.Configuration.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BudgetUnderControl.Modules.Transactions.Application.Commands.Tags.GetAllTags
{
    internal class GetAllTagsQueryHandler : IQueryHandler<GetAllTagsQuery, List<TagDTO>>
    {
        private readonly ITagService tagService;

        public GetAllTagsQueryHandler(ITagService tagService)
        {
            this.tagService = tagService;
        }
        public async Task<List<TagDTO>> Handle(GetAllTagsQuery request, CancellationToken cancellationToken)
        {
            var tags = await this.tagService.GetTagsAsync();
            return tags.ToList();
        }
    }
}