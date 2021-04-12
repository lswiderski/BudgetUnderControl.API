using BudgetUnderControl.Common.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace BudgetUnderControl.Shared.Infrastructure.Settings
{
    public class GeneralSettings
    {
        public string AppName { get; set; }
        public ApplicationType ApplicationType { get; set; }
        public string ApplicationId { get; set; }

        /*
        public GeneralSettings InjectEnvVariables()
        {
            this.BUC_DB_Address = Environment.GetEnvironmentVariable(nameof(this.BUC_DB_Address)) ?? this.BUC_DB_Address;
            this.BUC_DB_Name = Environment.GetEnvironmentVariable(nameof(this.BUC_DB_Name)) ?? this.BUC_DB_Name;
            this.BUC_DB_User = Environment.GetEnvironmentVariable(nameof(this.BUC_DB_User)) ?? this.BUC_DB_User;
            this.BUC_DB_Password = Environment.GetEnvironmentVariable(nameof(this.BUC_DB_Password)) ?? this.BUC_DB_Password;
            return this;
        }*/
    }
}
