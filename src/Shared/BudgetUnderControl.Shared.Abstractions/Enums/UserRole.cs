using BudgetUnderControl.Shared.Abstractions.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace BudgetUnderControl.Common.Enums
{
    public enum UserRole : int
    {
        [StringValue("User")]
        User = 1,

        [StringValue("LimitedUser")]
        LimitedUser = 2,

        [StringValue("PremiumUser")]
        PremiumUser = 3,

        [StringValue("Admin")]
        Admin = 99
    }
}
