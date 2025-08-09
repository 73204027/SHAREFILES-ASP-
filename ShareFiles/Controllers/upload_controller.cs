public static class upload_controller {
    public static void MapUploadEndpoints(this IEndpointRouteBuilder routes) {
        routes.MapPost("/upload", async (HttpRequest request, fileUploadHandler_service service) => //why don't you directly call the service method here??
        {
            try
            {
                var files = request.Form.Files;

                if (files.Count == 0){
                    return Results.BadRequest(new {error="No files uploaded"});
                };

            var savedPaths = new List<string>();
            var skippedFiles = new List<string>();


            // execute service method to all files
            foreach (var file in files){
                if (file==null || file.Length==0){
                    skippedFiles.Add(file?.FileName ?? "Unnamed file");
                    continue;
                }

                var savedPath = await service.SaveFileAsync(file);
                savedPaths.Add(savedPath);
            }

            return Results.Ok(new
            {
                paths = savedPaths,
                skipped = skippedFiles,
                message = skippedFiles.Count > 0 ? $"{skippedFiles.Count} files were empty":"All files were succesfully saved"
            });
            }
            catch(Exception ex)
            {
                return Results.BadRequest(new { error = ex.Message });
            }
        });
    }
}
