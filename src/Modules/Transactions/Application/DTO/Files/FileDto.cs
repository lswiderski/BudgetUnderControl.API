using System;

namespace BudgetUnderControl.Modules.Transactions.Application.DTO
{
    public class FileDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string ContentType { get; set; }

        public string FilePath { get; set; }
    }
}
