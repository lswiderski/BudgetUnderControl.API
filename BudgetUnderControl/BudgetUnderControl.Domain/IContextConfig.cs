﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetUnderControl.Domain
{
    public interface IContextConfig
    {
        string DBPath { get; set; }
    }
}
