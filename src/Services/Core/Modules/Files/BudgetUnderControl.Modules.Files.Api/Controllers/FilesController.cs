using System;
using System.Linq;
using System.Threading.Tasks;
using BudgetUnderControl.Modules.Files.Core;
using BudgetUnderControl.Modules.Files.Core.Commands.Files.DeleteFile;
using BudgetUnderControl.Modules.Files.Core.Commands.Files.GetFile;
using BudgetUnderControl.Modules.Files.Core.Commands.Files.SaveFile;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BudgetUnderControl.Modules.Files.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FilesController : ControllerBase
    {
        private readonly IFilesModule filesModule;

        public FilesController(IFilesModule filesModule)
        {
            this.filesModule = filesModule;
        }


        // GET api/files/552cbd7c-e9d9-46c9-ab7e-2b10ae38ab4a
        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(Guid id, string token)
        {
            if (string.IsNullOrWhiteSpace(token))
            {
                return BadRequest();
            }

            //TODO token validation
            var file = await this.filesModule.ExecuteQueryAsync(new GetFileQuery(id, token));

            if (file == null)
            {
                return NotFound();
            }
            return PhysicalFile(file.FilePath, file.ContentType, $"{file.Name}");
        }

        // POST api/files
        [HttpPost]
        [Authorize]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Post(IFormCollection files)
        {
            var file = files.Files.FirstOrDefault();

            if (file == null || file.Length == 0)
            {
                return BadRequest();
            }
          
            var fileId = await filesModule.ExecuteCommandAsync(new SaveFileCommand(file));

            return Ok(fileId);
        }

        // DELETE api/files/552cbd7c-e9d9-46c9-ab7e-2b10ae38ab4a
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(Guid id)
        {
            await filesModule.ExecuteCommandAsync(new DeleteFileCommand(id));
            return NoContent();
        }
    }
}
