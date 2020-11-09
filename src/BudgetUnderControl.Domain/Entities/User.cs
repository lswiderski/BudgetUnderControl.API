using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BudgetUnderControl.Domain
{
    public class User : ISyncable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; protected set; }
        [StringLength(50)]
        public string Username { get; protected set; }
        public string FirstName { get; protected set; }
        public string LastName { get; set; }
        public string Role { get; protected set; }
        [StringLength(150)]
        public string Email { get; protected set; }
        public string Password { get; protected set; }
        public string Salt { get; protected set; }
        public DateTime CreatedAt { get; protected set; }
        public DateTime? ModifiedOn { get; protected set; }
        public Guid ExternalId { get; protected set; }
        public bool IsDeleted { get; protected set; }
        public bool IsActivated { get; set; }
        public DateTime? ActivatedOn { get; protected set; }
        [StringLength(50)]
        public string ActivationCode { get; set; }

        public List<Account> Accounts { get; protected set; }
        public List<AccountGroup> AccountGroups { get; protected set; }
        public List<Transaction> Transactions { get; protected set; }


        protected User()
        {

        }

        public static User Create(string login, string firstName, string lastName, string role, string email, string password, string salt,
            bool isActive = true, Guid? externalId = null)
        {
            return new User()
            {
                Username = login,
                Role = role,
                Email = email.ToLower(),
                Password = password,
                Salt = salt,
                ExternalId = externalId ?? Guid.NewGuid(),
                ModifiedOn = DateTime.UtcNow,
                CreatedAt = DateTime.UtcNow,
                IsDeleted = !isActive,
                IsActivated = false,
                FirstName = firstName,
                LastName = lastName,
                ActivationCode = Guid.NewGuid().ToString()
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

        public void EditExternalId(Guid newId)
        {
            this.ExternalId = newId;
            this.UpdateModify();
        }

        public bool Active(string code)
        {
            if(code == this.ActivationCode)
            {
                this.IsActivated = true;
                this.ActivatedOn = DateTime.UtcNow;
                this.UpdateModify();
                return true;
            }

            return false;
        }
    }
}
