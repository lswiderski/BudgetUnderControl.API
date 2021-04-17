using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BudgetUnderControl.Modules.Users.Domain.Enums;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace BudgetUnderControl.Modules.Users.Domain.Entities
{
    public class Token
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; protected set; }

        public Guid UserId { get; set; }

        public TokenType Type { get; set; }

        public string Code { get; set; }

        public DateTime ValidUntil { get; set; }

        public DateTime CreatedOn { get; set; }

        public bool IsValid { get; set; }
        
        public virtual User User { get; set; }

        public static Token Create(TokenType type, Guid  userId, DateTime validUntil, string code = null)
        {
            return new Token
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                Type = type,
                ValidUntil = validUntil,
                Code = code ?? Guid.NewGuid().ToString(),
                CreatedOn = DateTime.UtcNow,
                IsValid = true
            };
        }

        public void Devalidate(bool isValid = false)
        {
            this.IsValid = isValid;

            if(!isValid)
            {
                this.ValidUntil = DateTime.UtcNow;
            }
        }
    }
}
