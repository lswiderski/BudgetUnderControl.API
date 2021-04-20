using System.Threading;
using System.Threading.Tasks;
using BudgetUnderControl.Modules.Files.Core.DTO;
using BudgetUnderControl.Modules.Files.Core.Services;
using BudgetUnderControl.Shared.Application.CQRS.Configuration.Queries;

namespace BudgetUnderControl.Modules.Files.Core.Commands.Files.GetFileContent
{
   internal class GetFileContentQueryHandler : IQueryHandler<GetFileContentQuery, byte[]>
   {
       private readonly IFileService _fileService;

       internal  GetFileContentQueryHandler(IFileService fileService)
       {
           this._fileService = fileService;
       }
       public async Task<byte[]> Handle( GetFileContentQuery request, CancellationToken cancellationToken)
       {
           var bytes = await this._fileService.GetFileBytesAsync(request.FileId);
           return bytes;
       }
   }
}