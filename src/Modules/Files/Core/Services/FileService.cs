using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BudgetUnderControl.Modules.Files.Core.DataAccess;
using BudgetUnderControl.Modules.Files.Core.DataAccess.Repositories;
using BudgetUnderControl.Modules.Files.Core.DTO;
using BudgetUnderControl.Shared.Abstractions.Contexts;
using BudgetUnderControl.Shared.Infrastructure.Settings;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace BudgetUnderControl.Modules.Files.Core.Services
{
    public class FileService : IFileService
    {
        private string _uploadCatalog = "uploads";

        private readonly IContext context;
        private readonly FilesModuleSettings settings;
        private readonly ILogger<FileService> logger;
        private readonly IFilesRepository _filesRepository;

        public FileService(FilesDbContext filesContext, IContext context, FilesModuleSettings settings, ILogger<FileService> logger, IFilesRepository filesRepository)
        {
            this.context = context;
            this.settings = settings;
            this.logger = logger;
            _filesRepository = filesRepository;
        }

        public async Task<Guid> SaveFileAsync(IFormFile file)
        {
            var createdOn = DateTime.UtcNow;
            var rootPath = settings.FileRootPath;
            var uploadsRootFolder = Path.Combine(rootPath, _uploadCatalog, context.Identity.Id.ToString(), createdOn.Year.ToString(), createdOn.Month.ToString());
            if (!Directory.Exists(uploadsRootFolder))
            {
                Directory.CreateDirectory(uploadsRootFolder);
            }

            var id = Guid.NewGuid();

            var fileEntity = new Entities.File
            {
                ContentType = file.ContentType,
                FileName = file.FileName,
                UserId = this.context.Identity.Id,
                CreatedOn = createdOn,
                Id = id,
                ModifiedOn = createdOn,
                IsDeleted = false,
            };
            await this._filesRepository.AddAsync(fileEntity);

            var filePath = Path.Combine(uploadsRootFolder, fileEntity.Id.ToString());
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream).ConfigureAwait(false);
            }

            return fileEntity.Id;
        }

        public async Task<string> SaveFileContentAsync(byte[] content, Guid id, DateTime? date = null)
        {
            var createdOn = date ?? DateTime.UtcNow;
            var rootPath = settings.FileRootPath;
            var uploadsRootFolder = Path.Combine(rootPath, _uploadCatalog, context.Identity.Id.ToString(), createdOn.Year.ToString(), createdOn.Month.ToString());
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
            var fileEntity = await this._filesRepository.GetAsync(id);

            if (fileEntity != null)
            {
                // TODO there is no context avaiable now, check why
                var filePath = Path.Combine(rootPath, _uploadCatalog, fileEntity.UserId.ToString(), fileEntity.CreatedOn.Year.ToString(), fileEntity.CreatedOn.Month.ToString(), id.ToString());

                var file = new FileDto
                {
                    ContentType = fileEntity.ContentType,
                    Name = fileEntity.FileName,
                    Id = id,
                    FilePath = filePath,
                    CreatedOn =  fileEntity.CreatedOn,
                    ModifiedOn = fileEntity.ModifiedOn,
                    IsDeleted = fileEntity.IsDeleted,
                };
                return file;
            }
            return null;
        }
        
        public async Task<List<FileDto>> GetFilesAsync(Guid userId, DateTime changedSince)
        {

            var rootPath = settings.FileRootPath;
            var filesEntities = await this._filesRepository.GetAsync(userId,changedSince);

            if (filesEntities != null && filesEntities.Any())
            {
                var files = new List<FileDto>();
                foreach (var entity in filesEntities)
                {
                    var filePath = Path.Combine(rootPath, _uploadCatalog, context.Identity.Id.ToString(), entity.CreatedOn.Year.ToString(), entity.CreatedOn.Month.ToString(), entity.Id.ToString());

                    var file = new FileDto
                    {
                        ContentType = entity.ContentType,
                        Name = entity.FileName,
                        Id = entity.Id,
                        FilePath = filePath,
                        CreatedOn =  entity.CreatedOn,
                        ModifiedOn = entity.ModifiedOn,
                         IsDeleted = entity.IsDeleted,
                    };
                    
                    files.Add(file);
                }
                return files;
            }
            return null;
        }

        public async Task<byte[]> GetFileBytesAsync(Guid id)
        {
            var fileEntity = await this._filesRepository.GetAsync(id);
            if (fileEntity == null)
            {
                return null;
            }

            try
            {
                var rootPath = settings.FileRootPath;
                var filePath = Path.Combine(rootPath, _uploadCatalog, context.Identity.Id.ToString(), fileEntity.CreatedOn.Year.ToString(), fileEntity.CreatedOn.Month.ToString(), id.ToString());
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
            //check if user has access
            
            var fileEntity = await this._filesRepository.GetAsync(id);

            if (fileEntity == null)
            {
                return;
            }

            if (fileEntity.UserId != context.Identity.Id)
            {
                throw new UnauthorizedAccessException();
            }

            fileEntity.Delete();
            await this._filesRepository.UpdateAsync(fileEntity);
            this.RemoveFileContent(id, context.Identity.Id, fileEntity.CreatedOn);

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
