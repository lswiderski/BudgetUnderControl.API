using NLog;
using System;
using System.Collections.Generic;
using System.Text;

namespace BudgetUnderControl.Common
{
    public interface ILogManager
    {
        ILogger GetLog([System.Runtime.CompilerServices.CallerFilePath]string callerFilePath = "");
    }
}
