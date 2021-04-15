using BudgetUnderControl.Modules.Transactions.Application.Commands.Files.DeleteFile;
using BudgetUnderControl.Modules.Transactions.Application.Commands.Files.GetFile;
using BudgetUnderControl.Modules.Transactions.Application.Commands.Files.SaveFile;
using BudgetUnderControl.Modules.Transactions.Application.Contracts;
using BudgetUnderControl.Shared.Application.CQRS.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetUnderControl.Modules.Transactions.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FilesController : ControllerBase
    {
        private readonly ITransactionsModule _transactionsModule;

        public FilesController(ITransactionsModule transactionsModule)
        {
            _transactionsModule = transactionsModule;
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
            var file = await this._transactionsModule.ExecuteQueryAsync(new GetFileQuery(id, token));

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
          
            var fileId = await _transactionsModule.ExecuteCommandAsync(new SaveFileCommand(file));

            return Ok(fileId);
        }

        // DELETE api/files/552cbd7c-e9d9-46c9-ab7e-2b10ae38ab4a
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _transactionsModule.ExecuteCommandAsync(new DeleteFileCommand(id));
            return NoContent();
        }
    }
}
