﻿using BudgetUnderControl.Modules.Users.Application.DTO;
using BudgetUnderControl.Shared.Application.CQRS.Configuration.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetUnderControl.Modules.Users.Application.Commands.Users.GetUsers
{
    public class GetUsersQuery : QueryBase<IEnumerable<UserListItemDTO>>
    {
    }
}
