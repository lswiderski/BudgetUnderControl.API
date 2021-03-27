using System;
using BudgetUnderControl.Common.Enums;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BudgetUnderControl.Domain
{
    public class Transaction : ISyncable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; protected set; }
        public int AccountId { get; protected set; }
        public TransactionType Type { get; protected set; }
        public decimal Amount { get; protected set; }
        public DateTime Date { get; protected set; }
        public int? CategoryId { get; protected set; }
        public string Name { get; protected set; }
        public string Comment { get; protected set; }
        public DateTime CreatedOn { get; protected set; }
        public DateTime? ModifiedOn { get; protected set; }
        public Guid ExternalId { get; protected set; }
        public int AddedById { get; protected set; }
        public bool IsDeleted { get; protected set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }

        [NotMapped]
        public bool IsTransfer { get; set; }

        [NotMapped]
        public string ExternalIdAsString => ExternalId.ToString();

        public List<AccountSnapshot> AccountSnapshots { get; set; }
        public ICollection<TagToTransaction> TagsToTransaction { get; set; }
        public ICollection<FileToTransaction> FilesToTransaction { get; set; }
        public Category Category { get; set; }
        public Account Account { get; set; }
        public ICollection<Transfer> ToTransfers { get; set; }
        public ICollection<Transfer> FromTransfers { get; set; }
        public User AddedBy { get; protected set; }

        public Transaction()
        {
        }
        
        public static Transaction Create(int accountId, TransactionType type, decimal amount, DateTime date, string name, string comment, int addedById, bool isDeleted, int? categoryId = null, Guid? guid = null, double? latitude = null, double? longitude = null)
        {
            return new Transaction()
            {
                AccountId = accountId,
                Type = type,
                Amount = amount,
                Date = date,
                Name = name,
                Comment = comment,
                CategoryId = categoryId,
                CreatedOn = DateTime.UtcNow,
                ExternalId = guid ?? Guid.NewGuid(),
                AddedById = addedById,
                IsDeleted = isDeleted,
                ModifiedOn = DateTime.UtcNow,
                Latitude = latitude,
                Longitude = longitude
            };
        }

        public void Edit(int accountId, TransactionType type, decimal amount, DateTime date, string name, string comment, int addedById, bool isDeleted, int? categoryId = null, double? latitude = null, double? longitude = null)
        {
            this.AccountId = accountId;
            this.Type = type;
            this.Amount = amount;
            this.Date = date;
            this.Name = name;
            this.Comment = comment;
            this.CategoryId = categoryId;
            this.ModifiedOn = DateTime.UtcNow;
            this.AddedById = addedById;
            this.IsDeleted = isDeleted;
            this.Latitude = latitude;
            this.Longitude = longitude;
        }

        /// <summary>
        /// Use for sync/imports
        /// </summary>
        /// <param name="id"></param>
        public void SetId(int id)
        {
            this.Id = id;
        }

        /// <summary>
        /// Use for sync/imports
        /// </summary>
        /// <param name="date"></param>
        public void SetCreatedOn(DateTime date)
        {
            this.CreatedOn = date;
        }

        /// <summary>
        /// Use for sync/imports
        /// </summary>
        /// <param name="date"></param>
        public void SetModifiedOn(DateTime? date)
        {
            this.ModifiedOn = date;
        }

        public void Delete(bool delete = true)
        {
            this.IsDeleted = delete;
            if(delete == true)
            {
                this.Amount = 0;
            }
            
            this.UpdateModify();
        }

        public void UpdateModify()
        {
            this.ModifiedOn = DateTime.UtcNow;
        }
    }
}
