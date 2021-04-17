using System;
using System.Threading.Tasks;
using BudgetUnderControl.Modules.Users.Domain.Entities;
using BudgetUnderControl.Modules.Users.Domain.Enums;

namespace BudgetUnderControl.Modules.Users.Domain.Repositories
{
    public interface ITokenRepository
    {
        Task AddAsync(Token token);
        Task<Token> GetAsync(Guid id, Guid userId);
        Task<Token> GetByCodeAsync(string code, TokenType type, Guid userId);
        Task UpdateAsync(Token token);
        Task DeactivateTokensAsync(TokenType type, Guid userId);
    }
}
