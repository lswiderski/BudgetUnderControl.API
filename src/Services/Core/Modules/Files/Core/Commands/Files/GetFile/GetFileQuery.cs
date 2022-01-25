using System;
using BudgetUnderControl.Modules.Files.Core.DTO;
using BudgetUnderControl.Shared.Application.CQRS.Configuration.Queries;

namespace BudgetUnderControl.Modules.Files.Core.Commands.Files.GetFile
{
    public class GetFileQuery : QueryBase<FileDto>
    {
        public GetFileQuery(Guid fileId, string token)
        {
            FileId = fileId;
            Token = token;
        }

        public Guid FileId { get; }

        public string Token { get; }
    }
}
