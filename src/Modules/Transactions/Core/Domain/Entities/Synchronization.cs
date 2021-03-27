using BudgetUnderControl.Common.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BudgetUnderControl.Domain
{
    public class Synchronization
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public DateTime LastSyncAt { get; set; }
        public int UserId { get; set; }
        public SynchronizationComponent Component { get; set; }
        public Guid ComponentId { get; set; }
        public virtual User User { get; set; }
    }
}
