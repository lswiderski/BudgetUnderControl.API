using System;
using System.Collections.Generic;
using BudgetUnderControl.Modules.Files.Core.DTO;
using BudgetUnderControl.Shared.Application.CQRS.Configuration.Queries;

namespace BudgetUnderControl.Modules.Files.Core.Commands.Files.GetFile
{
    public class GetFilesQuery : QueryBase<List<FileDto>>
    {
        public GetFilesQuery(Guid userId, DateTime changedSince)
        {
            UserId = userId;
            changedSince = changedSince;
        }

        public Guid UserId { set; get; }

        public DateTime ChangedSince { set; get; }
    }
}
