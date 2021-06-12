using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetUnderControl.Shared.Infrastructure.Exceptions
{
    public class ValidationCommandException : Exception
    {
        public List<ValidationCommandExceptionError> Errors { get; }

        public ValidationCommandException(List<ValidationCommandExceptionError> errors)
        {
            this.Errors = errors;
        }
    }

    public class ValidationCommandExceptionError
    {
        public string PropertyName { get; set; }
        public string ErrorMessage { get; set; }
        public object AttemptedValue { get; set; }
        public string ErrorCode { get; set; }
    }
}
