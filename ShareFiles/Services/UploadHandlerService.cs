using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading.Tasks;
using System.Linq;


namespace ShareFiles.Services
{

    public class UploadHandlerService(IWebHostEnvironment env)
    {

        private readonly string _storagePath = Path.Combine(env.ContentRootPath, "App_Data", "Storage");

        public string CreateDirectory()
        {
            var Today = DateOnly.FromDateTime(DateTime.Now);
            string TodayStr = Today.ToString("dd-MM-yyyy"); //safe for windows
            
            var DirsCreatedToday = new DirectoryInfo(_storagePath)
                .GetDirectories(searchPattern: TodayStr + "*");
            
            var OutputDir = DirsCreatedToday.Any() ? 
                $"{TodayStr} {DirsCreatedToday.Count()+1}"
                : $"{TodayStr} 1";

            //ensure that App_Data/Storage folder exists in root

            var OutputDirPath = Path.Combine(_storagePath, OutputDir);

            Directory.CreateDirectory(OutputDirPath);

            return OutputDirPath;
        }

        public async Task<string> SaveFileAsync(IFormFile file, string OutputDirPath)
        {
            // we don't handle name uniqueness, because iOS already does that, so we're sure everything is OK with file names

            
            var OutputPath = Path.Combine(OutputDirPath, file.FileName);

            // stream single file into server storage
            using (var stream = new FileStream(OutputPath, FileMode.Create))
            {
            await file.CopyToAsync(stream);
            }
            return file.FileName;

        }

    }
}