﻿using BudgetUnderControl.Common.Contracts;
using BudgetUnderControl.Common.Enums;
using BudgetUnderControl.Domain.Repositiories;
using BudgetUnderControl.Infrastructure.Commands;
using BudgetUnderControl.Infrastructure.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetUnderControl.Infrastructure.Services
{
    public class SyncRequestBuilder : ISyncRequestBuilder
    {

        private readonly ITransactionRepository transactionRepository;
        private readonly IAccountRepository accountRepository;
        private readonly ICurrencyRepository currencyRepository;
        private readonly ICategoryRepository categoryRepository;
        private readonly IAccountGroupRepository accountGroupRepository;
        private readonly IUserRepository userRepository;
        private readonly ISynchronizationRepository synchronizationRepository;
        private readonly IUserIdentityContext userIdentityContext;
        private readonly ITagRepository tagRepository;
        private readonly GeneralSettings settings;
        public SyncRequestBuilder(ITransactionRepository transactionRepository,
            IAccountRepository accountRepository,
            ICurrencyRepository currencyRepository,
            ICategoryRepository categoryRepository,
            IAccountGroupRepository accountGroupRepository,
            IUserRepository userRepository,
            ISynchronizationRepository synchronizationRepository,
            IUserIdentityContext userIdentityContext,
            ITagRepository tagRepository,
            GeneralSettings settings)
        {
            this.transactionRepository = transactionRepository;
            this.accountRepository = accountRepository;
            this.currencyRepository = currencyRepository;
            this.categoryRepository = categoryRepository;
            this.accountGroupRepository = accountGroupRepository;
            this.userRepository = userRepository;
            this.synchronizationRepository = synchronizationRepository;
            this.userIdentityContext = userIdentityContext;
            this.settings = settings;
            this.tagRepository = tagRepository;
        }

        public async Task<SyncRequest> CreateSyncRequestAsync(SynchronizationComponent source, SynchronizationComponent target)
        {
            //get
            var synchronizations = await this.synchronizationRepository.GetSynchronizationsAsync();
            var synchronization = synchronizations.Where(x => x.Component == target && x.UserId == userIdentityContext.UserId).FirstOrDefault();

            var request = new SyncRequest
            {
                Component = source,
                ComponentId = new Guid(settings.ApplicationId),
                UserId = userIdentityContext.ExternalId,
                LastSync = synchronization != null ? synchronization.LastSyncAt : new DateTime(),
            };

            //collect collections to send to update
            //modified on || created on > LastSync

            request = await this.GetCollectionsToSyncAsync(request);

            return request;
        }

        private async Task<SyncRequest> GetCollectionsToSyncAsync(SyncRequest syncRequest)
        {
            syncRequest.Transactions = await this.GetTransactionsToSyncAsync(syncRequest.LastSync);
            syncRequest.Transfers = await this.GetTransfersToSyncAsync(syncRequest.LastSync);
            syncRequest.Accounts = await this.GetAccountsToSyncAsync(syncRequest.LastSync);
            syncRequest.AccountGroups = await this.GetAccountGroupsToSyncAsync(syncRequest.LastSync);
            syncRequest.Users = await this.GetUsersToSyncAsync(syncRequest.LastSync);
            syncRequest.Categories = await this.GetCategoriesToSyncAsync(syncRequest.LastSync);
            syncRequest.Tags = await this.GetTagsToSyncAsync(syncRequest.LastSync);

            return syncRequest;
        }

        private async Task<IEnumerable<TransactionSyncDTO>> GetTransactionsToSyncAsync(DateTime changedSince)
        {
            var transactions = (await transactionRepository.GetTransactionsAsync(new TransactionsFilter { ChangedSince = changedSince, IncludeDeleted = true }))
                .Select(x => new TransactionSyncDTO
                {
                    Name = x.Name,
                    Comment = x.Comment,
                    AccountId = x.AccountId,
                    Amount = x.Amount,
                    CategoryId = x.CategoryId,
                    CreatedOn = x.CreatedOn,
                    Date = x.Date,
                    Id = x.Id,
                    ExternalId = x.ExternalId,
                    ModifiedOn = x.ModifiedOn,
                    Type = x.Type,
                    IsDeleted = x.IsDeleted,
                    Tags = x.TagsToTransaction.Select(y => new TagSyncDTO
                    {
                        ExternalId = y.Tag.ExternalId,
                        Id = y.Tag.Id,
                        IsDeleted = y.Tag.IsDeleted,
                        ModifiedOn = y.Tag.ModifiedOn,
                        Name = y.Tag.Name
                    }).ToList()
                }).ToList();

            var accounts = (await this.accountRepository.GetAccountsAsync()).ToDictionary(x => x.Id, x => x.ExternalId);
            var categories = (await this.categoryRepository.GetCategoriesAsync()).ToDictionary(x => x.Id, x => x.ExternalId);


            foreach (var transaction in transactions)
            {
                transaction.AccountExternalId = accounts[transaction.AccountId];
                if (transaction.CategoryId.HasValue)
                {
                    transaction.CategoryExternalId = categories[transaction.CategoryId.Value];
                }
            }

            return transactions;
        }

        private async Task<IEnumerable<TransferSyncDTO>> GetTransfersToSyncAsync(DateTime changedSince)
        {
            var transfers = (await this.transactionRepository.GetTransfersModifiedSinceAsync(changedSince)).Select(x => new TransferSyncDTO
            {
                Id = x.Id,
                FromTransactionId = x.FromTransactionId,
                Rate = x.Rate,
                ToTransactionId = x.ToTransactionId,
                ExternalId = x.ExternalId,
                IsDeleted = x.IsDeleted,
                ModifiedOn = x.ModifiedOn,
                ToTransactionExternalId = x.ToTransaction.ExternalId,
                FromTransactionExternalId = x.FromTransaction.ExternalId,
                 
            }).ToList();

            return transfers;
        }

        private async Task<IEnumerable<AccountSyncDTO>> GetAccountsToSyncAsync(DateTime changedSince)
        {
            var accounts = (await this.accountRepository.GetAccountsAsync())
                .Where(x => x.ModifiedOn >= changedSince)
                .Select(x => new AccountSyncDTO
            {
                Id = x.Id,
                ExternalId = x.ExternalId,
                AccountGroupId = x.AccountGroupId,
                Comment = x.Comment,
                CurrencyId = x.CurrencyId,
                IsIncludedToTotal = x.IsIncludedToTotal,
                Name = x.Name,
                Order = x.Order,
                ParentAccountId = x.ParentAccountId,
                Type = x.Type,
                ModifiedOn = x.ModifiedOn,
                IsDeleted = x.IsDeleted
            }).ToList();

            var allAccounts = (await this.accountRepository.GetAccountsAsync()).ToDictionary(x => x.Id, x => x.ExternalId);
            var accountGroups = (await this.accountGroupRepository.GetAccountGroupsAsync()).ToDictionary(x => x.Id, x => x.ExternalId);
            foreach (var account in accounts)
            {
                account.AccountGroupExternalId = accountGroups[account.AccountGroupId];
                if (account.ParentAccountId.HasValue)
                {
                    account.ParentAccountExternalId = allAccounts[account.ParentAccountId.Value];
                }
            }

            return accounts;
        }

        private async Task<IEnumerable<AccountGroupSyncDTO>> GetAccountGroupsToSyncAsync(DateTime changedSince)
        {
            var accountgroups = (await this.accountGroupRepository.GetAccountGroupsAsync())
                .Where(x => x.ModifiedOn >= changedSince)
                .Select(x => new AccountGroupSyncDTO
                {
                    Id = x.Id,
                    ExternalId = x.ExternalId,
                    Name = x.Name,
                    ModifiedOn = x.ModifiedOn,
                    IsDeleted = x.IsDeleted,
                    OwnerId = x.OwnerId,
                }).ToList();

            var userExternalId = (await this.userRepository.GetFirstUserAsync()).ExternalId;

            foreach (var account in accountgroups)
            {
                account.OwnerExternalId = userExternalId;
            }

            return accountgroups;
        }

        private async Task<IEnumerable<UserSyncDTO>> GetUsersToSyncAsync(DateTime changedSince)
        {
            //temporary I do not support multi users
            var user = await this.userRepository.GetFirstUserAsync();
            var result = new List<UserSyncDTO>();

            result.Add(new UserSyncDTO
            {
                ExternalId = user.ExternalId,
                Email = user.Email,
                ModifiedOn = user.ModifiedOn,
                CreatedAt = user.CreatedAt,
                Id = user.Id,
                IsDeleted = user.IsDeleted,
                Role = user.Role,
                Username = user.Username
            });
            

            return result;
        }

        private async Task<IEnumerable<CategorySyncDTO>> GetCategoriesToSyncAsync(DateTime changedSince)
        {
            var categories = (await this.categoryRepository.GetCategoriesAsync())
                .Where(x => x.ModifiedOn >= changedSince)
                .Select(x => new CategorySyncDTO
                {
                    Id = x.Id,
                    ExternalId = x.ExternalId,
                    Name = x.Name,
                    ModifiedOn = x.ModifiedOn,
                    IsDeleted = x.IsDeleted,
                    OwnerId = x.OwnerId,
                }).ToList();

            var userExternalId = (await this.userRepository.GetFirstUserAsync()).ExternalId;

            foreach (var category in categories)
            {
                category.OwnerExternalId = userExternalId;
            }

            return categories;
        }

        private async Task<IEnumerable<TagSyncDTO>> GetTagsToSyncAsync(DateTime changedSince)
        {
            var tags = (await this.tagRepository.GetAsync())
                .Where(x => x.ModifiedOn >= changedSince)
                .Select(x => new TagSyncDTO
                {
                    Id = x.Id,
                    ExternalId = x.ExternalId,
                    Name = x.Name,
                    ModifiedOn = x.ModifiedOn,
                    IsDeleted = x.IsDeleted
                }).ToList();

            return tags;
        }
    }
}