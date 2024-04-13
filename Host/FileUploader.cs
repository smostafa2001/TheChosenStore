using Common.Application;

namespace Host;

public class FileUploader(IWebHostEnvironment webHostEnvironment) : IFileUploader
{
    public string Upload(IFormFile file, string path)
    {
        if (file is null) return "";

        var directoryPath = $"{webHostEnvironment.WebRootPath}//UploadedFiles//{path}";
        if (!Directory.Exists(directoryPath)) Directory.CreateDirectory(directoryPath);

        var fileName = $"{DateTime.Now.ToFileName()}-{file.FileName}";
        var filePath = $"{directoryPath}//{fileName}";
        using var output = File.Create(filePath);
        file.CopyTo(output);
        return $"{path}/{fileName}";
    }
}
