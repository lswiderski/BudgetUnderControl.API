﻿using System;
using System.Collections.Generic;
using System.Text;

namespace BudgetUnderControl.CommonInfrastructure.Commands
{
    public class EditTag : ICommand
    {
        public string Name { get; set; }

        public Guid ExternalId { get; set; }

        public bool IsDeleted { get; set; }
    }
}
