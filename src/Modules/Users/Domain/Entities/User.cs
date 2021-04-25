using BudgetUnderControl.Common.Enums;
using BudgetUnderControl.Shared.Abstractions.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BudgetUnderControl.Modules.Users.Domain.Entities
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid Id { get; protected set; }
        [StringLength(50)]
        public string Username { get; protected set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Role { get; protected set; }
        [StringLength(150)]
        public string Email { get; protected set; }
        public string Password { get; protected set; }
        public string Salt { get; protected set; }
        public DateTime CreatedAt { get; protected set; }
        public DateTime? ModifiedOn { get; protected set; }
        public bool IsDeleted { get; protected set; }
        public bool IsActivated { get; set; }
        public DateTime? ActivatedOn { get; protected set; }
        
        public  virtual ICollection<Token> Tokens { get; protected set; }

        protected User()
        {

        }

        public static User Create(string login, string firstName, string lastName, string role, string email, string password, string salt,
            bool isActive = true, Guid? id = null)
        {
            return new User()
            {
                Username = login,
                Role = role,
                Email = email.ToLower(),
                Password = password,
                Salt = salt,
                Id = id ?? Guid.NewGuid(),
                ModifiedOn = DateTime.UtcNow,
                CreatedAt = DateTime.UtcNow,
                IsDeleted = !isActive,
                IsActivated = false,
                FirstName = firstName,
                LastName = lastName,
            };
        }


        public void Delete(bool delete = true)
        {
            this.IsDeleted = delete;
            this.UpdateModify();
        }

        public void UpdateModify()
        {
            this.ModifiedOn = DateTime.UtcNow;
        }

        public void EditUsername(string name)
        {
            if(this.Username != name)
            {
                this.Username = name;
                this.UpdateModify();
            }
        }

        public void EditEmail(string email)
        {
            if(this.Email != email)
            {
                this.Email = email;
                this.UpdateModify();
            }
        }

        public void EditRole(UserRole role)
        {
            if (this.Role != role.GetStringValue())
            {
                this.Role = role.GetStringValue();
                this.UpdateModify();
            }
        }

        public bool Activate()
        {
            if(!this.IsActivated)
            {
                this.IsActivated = true;
                this.ActivatedOn = DateTime.UtcNow;
                this.UpdateModify();
                return true;
            }

            return false;
        }

        public bool Deactivate()
        {
            if (this.IsActivated)
            {
                this.IsActivated = false;
                this.ActivatedOn = null;
                this.UpdateModify();
                return true;
            }

            return false;
        }
    }
}
