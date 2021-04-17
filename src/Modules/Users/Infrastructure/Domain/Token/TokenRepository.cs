using System;
using System.Linq;
using System.Threading.Tasks;
using BudgetUnderControl.Domain;
using BudgetUnderControl.Modules.Users.Domain.Enums;
using BudgetUnderControl.Modules.Users.Domain.Repositories;
using BudgetUnderControl.Modules.Users.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace BudgetUnderControl.Modules.Users.Infrastructure.Domain.Token
{
    public class TokenRepository :  ITokenRepository
    {
        private readonly UsersDbContext _context;

        public TokenRepository(UsersDbContext context) 
        {
            this._context = context;
        }

        public async Task AddAsync(Users.Domain.Entities.Token token)
        {
            await this._context.Tokens.AddAsync(token);
            await this._context.SaveChangesAsync();
        }

        public async Task<Users.Domain.Entities.Token> GetAsync(Guid id, Guid userId)
        {
            var token = await this._context.Tokens.FirstOrDefaultAsync(t => t.Id.Equals(id) && t.UserId == userId);

            return token;
        }

        public async Task<Users.Domain.Entities.Token> GetByCodeAsync(string code, TokenType type, Guid userId)
        {
            var token = await this._context.Tokens.FirstOrDefaultAsync(t => t.Code.Equals(code) && t.Type == type && t.UserId == userId);

            return token;
        }

        public async Task UpdateAsync(Users.Domain.Entities.Token token)
        {
            this._context.Tokens.Update(token);
            await this._context.SaveChangesAsync();
        }

        public async Task DeactivateTokensAsync(TokenType type, Guid userId)
        {
            var tokens = await this._context.Tokens
                .Where(t => t.Type == type 
            && t.UserId == userId
            && t.IsValid).ToListAsync();

            tokens.ForEach(x =>
            {
                x.IsValid = false;
                x.ValidUntil = DateTime.UtcNow;
            });

            await this._context.SaveChangesAsync();
        }
    }
}
