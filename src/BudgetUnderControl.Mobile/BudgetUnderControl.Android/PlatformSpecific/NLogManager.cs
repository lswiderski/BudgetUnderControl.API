﻿
using BudgetUnderControl.Common;
using BudgetUnderControl.Droid;
using NLog;
using NLog.Config;
using NLog.Targets;
using System;
using System.Globalization;
using System.IO;
using Xamarin.Forms;

[assembly: Dependency(typeof(NLogManager))]
namespace BudgetUnderControl.Droid
{
    public class NLogManager : ILogManager
    {
        private static Logger Logger;
        public NLogManager()
        {
            var config = new LoggingConfiguration();

            var consoleTarget = new ConsoleTarget();
            config.AddTarget("console", consoleTarget);

            var consoleRule = new LoggingRule("*", LogLevel.Trace, consoleTarget);
            config.LoggingRules.Add(consoleRule);

            var fileTarget = new FileTarget();

            string folder = Android.OS.Environment.ExternalStorageDirectory.Path;

            var date = DateTime.UtcNow.Date.ToString("dd.MM.yyyy");
            fileTarget.FileName = Path.Combine(folder, "BudgetUnderControl", string.Format("buc-Log-{0}.txt", date));
            config.AddTarget("file", fileTarget);

            var fileRule = new LoggingRule("*", LogLevel.Info, fileTarget);
            config.LoggingRules.Add(fileRule);

            LogManager.Configuration = config;
        }

        public ILogger GetLog([System.Runtime.CompilerServices.CallerFilePath] string callerFilePath = "")
        {
            string fileName = callerFilePath;

            if (fileName.Contains("/"))
            {
                fileName = fileName.Substring(fileName.LastIndexOf("/", StringComparison.CurrentCultureIgnoreCase) + 1);
            }

            Logger = LogManager.GetLogger(fileName);
            return Logger;
        }
    }
}