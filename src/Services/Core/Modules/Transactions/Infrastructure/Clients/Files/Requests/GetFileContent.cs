using System;

namespace BudgetUnderControl.Modules.Transactions.Infrastructure.Clients.Files.Requests
{
    public class GetFileContent
    {
        public GetFileContent(Guid fileId)
        {
            FileId = fileId;
        }

        public Guid FileId { get; set; }
    }
}