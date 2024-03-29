﻿using System;
using System.Collections.Generic;
using System.Text;

namespace BudgetUnderControl.Common.Extensions
{
    public class InvalidCommandException : Exception
    {
        public List<string> Errors { get; }

        public InvalidCommandException(List<string> errors)
        {
            this.Errors = errors;
        }
    }
}
