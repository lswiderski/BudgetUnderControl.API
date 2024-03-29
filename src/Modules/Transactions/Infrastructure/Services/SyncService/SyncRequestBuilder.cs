﻿using BudgetUnderControl.Modules.Transactions.Application.DTO;
using BudgetUnderControl.Common.Enums;
using BudgetUnderControl.Domain.Repositiories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BudgetUnderControl.Modules.Transactions.Application.Services;
using BudgetUnderControl.Domain;
using BudgetUnderControl.Modules.Transactions.Application.Clients.Files;
using Microsoft.EntityFrameworkCore;
using BudgetUnderControl.Shared.Infrastructure.Settings;
using BudgetUnderControl.Modules.Transactions.Core.ValueObjects;
using Microsoft.Extensions.Logging;
using BudgetUnderControl.Shared.Abstractions.Contexts;
using BudgetUnderControl.Modules.Transactions.Application.Clients.Users;

namespace BudgetUnderControl.Modules.Transactions.Infrastructure.Services
{
    public class SyncRequestBuilder : ISyncRequestBuilder
    {
        private readonly ILogger<SyncRequestBuilder> logger;
        private readonly ITransactionRepository transactionRepository;
        private readonly IAccountRepository accountRepository;
        private readonly ICurrencyRepository currencyRepository;
        private readonly ICategoryRepository categoryRepository;
        private readonly ISynchronizationRepository synchronizationRepository;
        private readonly IContext context;
        private readonly ITagRepository tagRepository;
        private readonly GeneralSettings settings;
        private readonly TransactionsContext transactionsContext;
        private readonly IUsersApiClient userApiClient;
        private readonly IFilesApiClient _filesApiClient;

        public SyncRequestBuilder(TransactionsContext transactionContext, ITransactionRepository transactionRepository,
            IAccountRepository accountRepository,
            ICurrencyRepository currencyRepository,
            ICategoryRepository categoryRepository,
            ISynchronizationRepository synchronizationRepository,
            IContext context,
            ITagRepository tagRepository,
            ILogger<SyncRequestBuilder> logger,
            GeneralSettings settings,
            IUsersApiClient userApiClient, IFilesApiClient filesApiClient)
        {
            this.transactionRepository = transactionRepository;
            this.accountRepository = accountRepository;
            this.currencyRepository = currencyRepository;
            this.categoryRepository = categoryRepository;
            this.synchronizationRepository = synchronizationRepository;
            this.context = context;
            this.settings = settings;
            this.tagRepository = tagRepository;
            this.logger = logger;
            this.transactionsContext = transactionContext;
            this.userApiClient = userApiClient;
            _filesApiClient = filesApiClient;
        }

        public async Task<SyncRequest> CreateSyncRequestAsync(SynchronizationComponent source, SynchronizationComponent target)
        {
            //get
            var synchronizations = await this.synchronizationRepository.GetSynchronizationsAsync();
            var synchronization = synchronizations.Where(x => x.Component == target && x.OwnerId == context.Identity.Id).FirstOrDefault();

            var request = new SyncRequest
            {
                Component = source,
                ComponentId = new Guid(settings.ApplicationId),
                UserId = context.Identity.Id,
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
            syncRequest.Users = await this.GetUsersToSyncAsync(syncRequest.LastSync);
            syncRequest.Categories = await this.GetCategoriesToSyncAsync(syncRequest.LastSync);
            syncRequest.Tags = await this.GetTagsToSyncAsync(syncRequest.LastSync);
            syncRequest.ExchangeRates = await this.GetExhangeRatesToSyncAsync();
            syncRequest.Files = await this.GetFilesToSyncAsync(syncRequest.LastSync);

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
                    Latitude = x.Latitude,
                    Longitude = x.Longitude,
                    Tags = x.TagsToTransaction.Select(y => new TagSyncDTO
                    {
                        ExternalId = y.Tag.ExternalId,
                        Id = y.Tag.Id,
                        IsDeleted = y.Tag.IsDeleted,
                        ModifiedOn = y.Tag.ModifiedOn,
                        Name = y.Tag.Name
                    }).ToList(),
                    Files = x.FilesToTransaction.Select(y => new FileToTransactionSyncDTO
                    {
                        ExternalId = y.ExternalId,
                        FileId = y.FileId,
                        IsDeleted = y.IsDeleted,
                        ModifiedOn = y.ModifiedOn,
                        TransactionId = y.TransactionId,
                        Id = y.Id,
                    }).ToList(),
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
                    Comment = x.Comment,
                    CurrencyId = x.CurrencyId,
                    IsIncludedToTotal = x.IsIncludedToTotal,
                    Name = x.Name,
                    Order = x.Order,
                    ParentAccountId = x.ParentAccountId,
                    Type = x.Type,
                    ModifiedOn = x.ModifiedOn,
                    Icon = x.Icon,
                    IsDeleted = x.IsDeleted,
                    IsActive = x.IsActive,
                }).ToList();

            var allAccounts = (await this.accountRepository.GetAccountsAsync()).ToDictionary(x => x.Id, x => x.ExternalId);
            foreach (var account in accounts)
            {
                if (account.ParentAccountId.HasValue)
                {
                    account.ParentAccountExternalId = allAccounts[account.ParentAccountId.Value];
                }
            }

            return accounts;
        }

      
        private async Task<IEnumerable<UserSyncDTO>> GetUsersToSyncAsync(DateTime changedSince)
        {
            //temporary I do not support multi users
            var user = await this.userApiClient.GetCurrentUserContextAsync();
            var result = new List<UserSyncDTO>();

            result.Add(new UserSyncDTO
            {
                ExternalId = user.UserId,
                Email = user.Email,
                ModifiedOn = user.ModifiedOn,
                CreatedAt = user.CreatedAt,
                IsDeleted = user.IsDeleted,
                Role = user.Role.ToString(),
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
                    Icon = x.Icon,
                }).ToList();

            var userExternalId = context.Identity.Id;

            foreach (var category in categories)
            {
                category.OwnerExternalId = userExternalId;
            }
            //logger.Info("Sent Categories" + string.Join("; ", categories.Select(x => x.ExternalId)));
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

        private async Task<IEnumerable<FileSyncDTO>> GetFilesToSyncAsync(DateTime changedSince)
        {
            var filesFromModule = (await this._filesApiClient.GetFilesAsync(context.Identity.Id, changedSince));
            if (filesFromModule != null)
            {
                var files = filesFromModule
                    .Select(x => new FileSyncDTO
                    {
                        Id = x.Id,
                        ExternalId = x.Id,
                        FileName = x.Name,
                        ContentType = x.ContentType,
                        CreatedOn = x.CreatedOn,
                        ModifiedOn = x.ModifiedOn,
                        IsDeleted = x.IsDeleted,
                        UserId = context.Identity.Id,
                    })
                    .ToList();

                for (int i = 0; i < files.Count; i++)
                {
                    if (!files[i].IsDeleted)
                    {
                        //TODO get file form filesAPI or rather get files by range ids or just publish events
                        files[i].Content = await _filesApiClient.GetFileContentAsync(files[i].Id);
                    }

                }

                return files;
            }

            return new List<FileSyncDTO>();
        }

        private async Task<IEnumerable<ExchangeRateSyncDTO>> GetExhangeRatesToSyncAsync()
        {
            var rates = (await this.currencyRepository.GetExchangeRatesAsync())
                .Select(x => new ExchangeRateSyncDTO
                {
                    Date = x.Date,
                    Rate = x.Rate,
                    FromCurrency = x.FromCurrency.Code,
                    ToCurrency = x.ToCurrency.Code,
                    ExternalId = x.ExternalId,
                    IsDeleted = x.IsDeleted,
                    ModifiedOn = x.ModifiedOn,
                }).ToList();

            return rates;
        }
    }
}
