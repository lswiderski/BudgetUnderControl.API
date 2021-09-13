using System.IO;

namespace BudgetUnderControl.Modules.Exporter.Core.DTO
{
    public class TransactionsReport
    {
        public byte [] Content { get; set; }
        
        public string ContentType { get; set; }
        
        public string Name { get; set; }
    }
}