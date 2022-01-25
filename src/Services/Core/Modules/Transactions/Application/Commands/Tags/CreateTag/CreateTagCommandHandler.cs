using BudgetUnderControl.Modules.Transactions.Application.Services;
using BudgetUnderControl.Shared.Application.CQRS.Configuration.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BudgetUnderControl.Modules.Transactions.Application.Commands.Tags.CreateTag
{
    internal class CreateTagCommandHandler : ICommandHandler<CreateTagCommand, Guid>
    {
        private readonly ITagService tagService;

        internal CreateTagCommandHandler(ITagService tagService)
        {
            this.tagService = tagService;
        }

        public async Task<Guid> Handle(CreateTagCommand request, CancellationToken cancellationToken)
        {
            await this.tagService.AddTagAsync(request);

            return request.ExternalId;
        }
    }
}