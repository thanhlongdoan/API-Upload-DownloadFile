using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace API_File.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        [HttpPost("Upload")]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            try
            {
                var pathFile = Guid.NewGuid() + "_" + file.FileName;
                var path = Path.Combine(Directory.GetCurrentDirectory(), "File", pathFile);
                var stream = new FileStream(path, FileMode.Create);
                await file.CopyToAsync(stream);
                return Content("Upload success");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet("DownloadFile")]
        public async Task<IActionResult> DownloadFile(string filename)
        {
            if (filename == null)
                return Content("filename empty");
            else
            {
                var path = Path.Combine(
                           Directory.GetCurrentDirectory(),
                           "File", filename);

                var memory = new MemoryStream();
                var stream = new FileStream(path, FileMode.Open);
                await stream.CopyToAsync(memory);
                memory.Position = 0;
                return File(memory, "application/octet-stream", Path.GetFileName(path));
            }
        }
    }
}