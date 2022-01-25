using System;

namespace BudgetUnderControl.Modules.Transactions.Application.Clients.Files.DTO
{
    public class FileDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string ContentType { get; set; }

        public string FilePath { get; set; }
        
        public DateTime CreatedOn { get; set; }
        
        public DateTime? ModifiedOn { get; set; }
        
        public  bool IsDeleted { get; set; }
    }
}