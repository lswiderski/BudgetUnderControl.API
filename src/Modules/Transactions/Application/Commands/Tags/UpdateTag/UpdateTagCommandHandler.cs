using BudgetUnderControl.CommonInfrastructure;
using BudgetUnderControl.Modules.Transactions.Application.Configuration.Commands;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BudgetUnderControl.Modules.Transactions.Application.Commands.Tags.UpdateTag
{
    internal class UpdateTagCommandHandler : ICommandHandler<UpdateTagCommand>
    {
        private readonly ITagService tagService;

        internal UpdateTagCommandHandler(ITagService tagService)
        {
            this.tagService = tagService;
        }

        public async Task<Unit> Handle(UpdateTagCommand request, CancellationToken cancellationToken)
        {
            var commandDTO = new CommonInfrastructure.Commands.EditTag
            {
                ExternalId = request.ExternalId,
                Name = request.Name,
                IsDeleted = request.IsDeleted,
            };
            await this.tagService.EditTagAsync(commandDTO);

            return Unit.Value;
        }

    }
}