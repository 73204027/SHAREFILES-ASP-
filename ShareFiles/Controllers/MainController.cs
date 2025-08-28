using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShareFiles.Services;
using System.Threading.Tasks;

namespace ShareFiles.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MainController : ControllerBase
    {
        private readonly UploadHandlerService _service;

        public MainController(UploadHandlerService service)
        {
            _service = service;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> Upload([FromForm] List<IFormFile> files)
        {
            if (files == null || files.Count == 0){
                return BadRequest("No files reached the server");
            }

            var OutputDirPath = _service.CreateDirectory();

            var successfullyUploadedFiles = 0;
            var unsuccessfullyUploadedFiles = 0;

            foreach (var file in files)
            {
                if (file == null || file.Length == 0)
                {
                    unsuccessfullyUploadedFiles++;
                }
                else
                {
                    var savedFile_name = await _service.SaveFileAsync(file, OutputDirPath);
                    successfullyUploadedFiles++;
                }  
            }



            return Ok(new
            {
                Uploaded_Files = successfullyUploadedFiles,
                Skipped_Files = unsuccessfullyUploadedFiles,
                Success_Rate = $"{((double)successfullyUploadedFiles / files.Count) * 100:F2}%"
            }
            );
        }



    }
}