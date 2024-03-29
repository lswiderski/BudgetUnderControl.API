﻿using BudgetUnderControl.Common.Enums;
using BudgetUnderControl.Domain;
using BudgetUnderControl.Domain.Repositiories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BudgetUnderControl.Infrastructure.Repositories
{
    public class SynchronizationRepository :  ISynchronizationRepository
    {
        private readonly TransactionsContext Context;

        public SynchronizationRepository(TransactionsContext context) 
        {
            this.Context = context;
        }

        public async Task<IEnumerable<Synchronization>> GetSynchronizationsAsync()
        {
            var list = await this.Context.Synchronizations.ToListAsync();

            return list;
        }

        public async Task AddSynchronizationAsync(Synchronization synchronization)
        {
            this.Context.Synchronizations.Add(synchronization);
            await this.Context.SaveChangesAsync();
        }

        public async Task<Synchronization> GetSynchronizationAsync(SynchronizationComponent component, Guid componentId, Guid userId)
        {
            var currency = await this.Context.Synchronizations.FirstOrDefaultAsync(x => x.Component == component && x.ComponentId == componentId && x.OwnerId == userId);

            return currency;
        }

        public async Task UpdateAsync(Synchronization synchronization)
        {
            this.Context.Synchronizations.Update(synchronization);
            await this.Context.SaveChangesAsync();
        }

        public async Task ClearSynchronizationAsync()
        {
            var entities = await this.Context.Synchronizations.ToListAsync();
            this.Context.Synchronizations.RemoveRange(entities);
            await this.Context.SaveChangesAsync();
        }
    }
}
