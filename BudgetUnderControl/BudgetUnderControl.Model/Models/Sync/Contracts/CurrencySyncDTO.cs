﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetUnderControl.Model
{
    public class CurrencySyncDTO
    {

        public int Id { get; set; }

        public string Code { get; set; }

        public string FullName { get; set; }

        public short Number { get; set; }

        public string Symbol { get; set; }
    }
}
