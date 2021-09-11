﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BudgetUnderControl.Modules.Exporter.Core.Clients.Transactions;
using BudgetUnderControl.Modules.Exporter.Core.Clients.Transactions.DTO;
using BudgetUnderControl.Shared.Abstractions.Modules;

namespace BudgetUnderControl.Modules.Exporter.Application.Clients.Transactions
{
   internal sealed class TransactionsApiClient: ITransactionsApiClient
   {
       private readonly IModuleClient _client;

       public TransactionsApiClient(IModuleClient client)
       {
           _client = client;
       }
       
       public  async Task<TransactionListDataSource> GetTransactionsAsync(TransactionsFilterDTO filters)
           => await _client.SendAsync<TransactionListDataSource>("transactions/get", filters);
   }
}