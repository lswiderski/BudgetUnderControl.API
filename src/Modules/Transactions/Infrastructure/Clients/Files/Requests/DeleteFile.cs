using System;

namespace BudgetUnderControl.Modules.Transactions.Infrastructure.Clients.Files.Requests
{
    public class DeleteFile
    {
        public DeleteFile(Guid fileId)
        {
            FileId = fileId;
        }

        public  Guid FileId { get; set; }
    }
}