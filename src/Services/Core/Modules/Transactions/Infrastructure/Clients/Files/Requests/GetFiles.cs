using System;

namespace BudgetUnderControl.Modules.Transactions.Infrastructure.Clients.Files.Requests
{
    public class GetFiles
    {
        public GetFiles(Guid userId, DateTime changedSince)
        {
            UserId = userId;
            ChangedSince = changedSince;
        }

        public Guid UserId { get; }

        public DateTime ChangedSince { get; }
    }
}