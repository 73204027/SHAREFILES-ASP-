using Microsoft.AspNetCore.Http;


public class fileUploadHandler_service {

    private readonly string _imagesPath = Path.Combine("storage", "images");
    private readonly string _videosPath = Path.Combine("storage", "videos");


    public async Task<string> SaveFileAsync(IFormFile file)
    {
        
        


        // ensure folders exist
        Directory.CreateDirectory(_imagesPath);
        Directory.CreateDirectory(_videosPath);

        // Decide where to save
        string extension = Path.GetExtension(file.FileName).ToLowerInvariant();
        string outputDir = extension switch 
        {
            ".jpg" or ".jpeg" or ".png" or ".gif" or ".heic" or ".dng" or ".tiff" => _imagesPath,            
            ".mp4" or ".avi" or ".mov" or ".hevc" or ".mkv" => _videosPath,
            _ => throw new InvalidOperationException("Unsupported file extension")
        };
        string outputPath = Path.Combine(outputDir, file.FileName);

        // Generate unique name (we'll skip this because iOS gallery already manages this)
        // string newFileName = $"{Guid.NewGuid()}{extension}";
        // string fullPath = Path.Combine(targetDir, newFileName);

        // stream file to avoid high RAM usage,     another advantage of streaming is we can monitor progress
        using (var stream = new FileStream(outputPath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        return outputPath;
    }
}
        
    
