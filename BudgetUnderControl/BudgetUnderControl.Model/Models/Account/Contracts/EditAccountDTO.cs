﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetUnderControl.Model
{
    public class EditAccountDTO
    {
        public int Id { get; set; }
        public int CurrencyId { get; set; }
        public string Currency { get; set; }
        public string CurrencySymbol { get; set; }
        public int AccountGroupId { get; set; }
        public string Name { get; set; }
        public decimal Amount { get; set; }
        public bool IsIncludedInTotal { get; set; }
        public string Comment { get; set; }

        public string AmountWithCurrency
        {
            get
            {
                return string.Format("{0}{1}", Amount, CurrencySymbol);
            }
        }
    }
}