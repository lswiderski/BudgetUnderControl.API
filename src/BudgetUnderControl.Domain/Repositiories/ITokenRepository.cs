using BudgetUnderControl.Common.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BudgetUnderControl.Domain.Repositiories
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
