using BudgetUnderControl.Common.Enums;
using BudgetUnderControl.Modules.Transactions.Application.Services;
using BudgetUnderControl.Domain;
using BudgetUnderControl.Domain.Repositiories;
using BudgetUnderControl.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetUnderControl.ApiInfrastructure.Repositories
{
    public class TokenRepository :  ITokenRepository
    {
        private readonly TransactionsContext Context;

        public TokenRepository(TransactionsContext context) 
        {
            this.Context = context;
        }

        public async Task AddAsync(Token token)
        {
            await this.Context.Tokens.AddAsync(token);
            await this.Context.SaveChangesAsync();
        }

        public async Task<Token> GetAsync(Guid id, Guid userId)
        {
            var token = await this.Context.Tokens.FirstOrDefaultAsync(t => t.Id.Equals(id) && t.UserExternalId == userId);

            return token;
        }

        public async Task<Token> GetByCodeAsync(string code, TokenType type, Guid userId)
        {
            var token = await this.Context.Tokens.FirstOrDefaultAsync(t => t.Code.Equals(code) && t.Type == type && t.UserExternalId == userId);

            return token;
        }

        public async Task UpdateAsync(Token token)
        {
            this.Context.Tokens.Update(token);
            await this.Context.SaveChangesAsync();
        }

        public async Task DeactivateTokensAsync(TokenType type, Guid userId)
        {
            var tokens = await this.Context.Tokens
                .Where(t => t.Type == type 
            && t.UserExternalId == userId
            && t.IsValid).ToListAsync();

            tokens.ForEach(x =>
            {
                x.IsValid = false;
                x.ValidUntil = DateTime.UtcNow;
            });

            await this.Context.SaveChangesAsync();
        }
    }
}
