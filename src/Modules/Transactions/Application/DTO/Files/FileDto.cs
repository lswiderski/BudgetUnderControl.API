using System;
using System.Collections.Generic;
using System.Text;

namespace BudgetUnderControl.Common.Contracts
{
    public class FileDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string ContentType { get; set; }

        public string FilePath { get; set; }
    }
}
