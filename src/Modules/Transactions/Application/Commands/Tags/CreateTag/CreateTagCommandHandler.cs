using BudgetUnderControl.CommonInfrastructure;
using BudgetUnderControl.Modules.Transactions.Application.Configuration.Commands;
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
            var commandDTO = new CommonInfrastructure.Commands.AddTag
            {
                ExternalId = request.ExternalId,
                Name = request.Name,
            };
            await this.tagService.AddTagAsync(commandDTO);

            return commandDTO.ExternalId;
        }
    }
}