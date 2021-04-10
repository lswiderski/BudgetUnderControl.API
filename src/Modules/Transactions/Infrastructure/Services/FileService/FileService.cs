using BudgetUnderControl.Modules.Transactions.Application.DTO;
using BudgetUnderControl.Domain;
using BudgetUnderControl.Shared.Infrastructure.Settings;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BudgetUnderControl.Modules.Transactions.Application.Services;
using Microsoft.Extensions.Logging;

namespace BudgetUnderControl.Modules.Transactions.Infrastructure.Services
{
    public class FileService : IFileService
    {
        private string _uploadCatalog = "uploads";

        private readonly IUserIdentityContext userIdentityContext;
        private readonly GeneralSettings settings;
        private readonly ILogger logger;
        private readonly TransactionsContext Context;

        public FileService(TransactionsContext context, IUserIdentityContext userIdentityContext, GeneralSettings settings, ILogger logger)
        {
            this.userIdentityContext = userIdentityContext;
            this.settings = settings;
            this.logger = logger;
            this.Context = context;
        }

        public async Task<Guid> SaveFileAsync(IFormFile file)
        {
            var createdOn = DateTime.UtcNow;
            var rootPath = settings.FileRootPath;
            var uploadsRootFolder = Path.Combine(rootPath, _uploadCatalog, userIdentityContext.ExternalId.ToString(), createdOn.Year.ToString(), createdOn.Month.ToString());
            if (!Directory.Exists(uploadsRootFolder))
            {
                Directory.CreateDirectory(uploadsRootFolder);
            }

            var id = Guid.NewGuid();

            var fileEntity = new Domain.File
            {
                ContentType = file.ContentType,
                FileName = file.FileName,
                UserId = userIdentityContext.ExternalId,
                CreatedOn = createdOn,
                ExternalId = id,
                Id = id,
                ModifiedOn = createdOn,
                IsDeleted = false,
            };

            this.Context.Files.Add(fileEntity);
            await this.Context.SaveChangesAsync();
            fileEntity.ExternalId = fileEntity.Id;
            await this.Context.SaveChangesAsync();

            var filePath = Path.Combine(uploadsRootFolder, fileEntity.Id.ToString());
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream).ConfigureAwait(false);
            }

            return fileEntity.Id;
        }

        public async Task<string> SaveFileAsync(byte[] content, Guid id, DateTime? date = null)
        {
            var createdOn = date ?? DateTime.UtcNow;
            var rootPath = settings.FileRootPath;
            var uploadsRootFolder = Path.Combine(rootPath, _uploadCatalog, userIdentityContext.ExternalId.ToString(), createdOn.Year.ToString(), createdOn.Month.ToString());
            if (!Directory.Exists(uploadsRootFolder))
            {
                Directory.CreateDirectory(uploadsRootFolder);
            }

            var filePath = Path.Combine(uploadsRootFolder, id.ToString());
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await fileStream.WriteAsync(content, 0, content.Length).ConfigureAwait(false);
            }

            return id.ToString();
        }


        public async Task<FileDto> GetFileAsync(Guid id, string token)
        {
            if (string.IsNullOrWhiteSpace(token))
            {
                return null;
            }
            var rootPath = settings.FileRootPath;
            var fileEntity = this.Context.Files.Where(x => x.Id == id).FirstOrDefault();

            if (fileEntity != null)
            {
                var filePath = Path.Combine(rootPath, _uploadCatalog, userIdentityContext.ExternalId.ToString(), fileEntity.CreatedOn.Year.ToString(), fileEntity.CreatedOn.Month.ToString(), id.ToString());

                var file = new FileDto
                {
                    ContentType = fileEntity.ContentType,
                    Name = fileEntity.FileName,
                    Id = id,
                    FilePath = filePath,
                };
                return file;
            }

            return null;

        }

        public async Task<byte[]> GetFileBytesAsync(Guid id)
        {
            var fileEntity = this.Context.Files.Where(x => x.Id == id).FirstOrDefault();
            if (fileEntity == null)
            {
                return null;
            }

            try
            {
                var rootPath = settings.FileRootPath;
                var filePath = Path.Combine(rootPath, _uploadCatalog, userIdentityContext.ExternalId.ToString(), fileEntity.CreatedOn.Year.ToString(), fileEntity.CreatedOn.Month.ToString(), id.ToString());
                using (var stream = new FileStream(filePath, FileMode.Open))
                {
                    using (MemoryStream memStream = new MemoryStream())
                    {
                        await stream.CopyToAsync(memStream);
                        return memStream.ToArray();
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Log(LogLevel.Error, ex, "Exception of file access");
                return null;
            }
        }

        public async Task RemoveFileAsync(Guid id)
        {
            //check if user hass access

            //remove connections with other objects like transactions
            var fileEntity = this.Context.Files.Where(x => x.Id == id).FirstOrDefault();

            if (fileEntity == null)
            {
                return;
            }

            if (fileEntity.UserId != userIdentityContext.ExternalId)
            {
                throw new UnauthorizedAccessException();
            }
            var f2t = this.Context.FilesToTransactions.Where(x => x.FileId == id).ToList();
            f2t.ForEach(x => x.Delete());
            fileEntity.Delete();
            await this.Context.SaveChangesAsync();
            this.RemoveFileContent(id, userIdentityContext.ExternalId, fileEntity.CreatedOn);

        }

        public void RemoveFileContent(Guid id, Guid userId, DateTime createdOn)
        {
            try
            {
                var rootPath = settings.FileRootPath;
                var filePath = Path.Combine(rootPath, _uploadCatalog, userId.ToString(), createdOn.Year.ToString(), createdOn.Month.ToString(), id.ToString());
                System.IO.File.Delete(filePath);
            }
            catch (Exception ex)
            {
                logger.Log(LogLevel.Error, ex, "Exception of file removing");
                throw;
            }

        }
    }
}
