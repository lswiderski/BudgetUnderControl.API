using BudgetUnderControl.Modules.Transactions.Application.DTO;
using BudgetUnderControl.Modules.Transactions.Application.Configuration.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetUnderControl.Modules.Transactions.Application.Commands.Files.GetFile
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
