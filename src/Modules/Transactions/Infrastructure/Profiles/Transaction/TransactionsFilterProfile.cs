using AutoMapper;
using BudgetUnderControl.Common.Contracts;
using BudgetUnderControl.Modules.Transactions.Core.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetUnderControl.Modules.Transactions.Infrastructure.Profiles.Transaction
{
    public class TransactionsFilterProfile : Profile
    {
        public TransactionsFilterProfile()
        {
            CreateMap< TransactionsFilterDTO, TransactionsFilter>();
        }

    }
}