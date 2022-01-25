using System;
using BudgetUnderControl.Shared.Application.CQRS.Configuration.Queries;

namespace BudgetUnderControl.Modules.Files.Core.Commands.Files.GetFileContent
{
    public class GetFileContentQuery : QueryBase<byte[]>
    {
        public GetFileContentQuery(Guid fileId)
        {
            FileId = fileId;
        }

        public Guid FileId { get; }
    }
}