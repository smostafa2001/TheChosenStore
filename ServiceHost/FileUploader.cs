using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using ShopManagement.Application.Contracts.Shared;
using ShopManagement.Domain.Shared;
using System;
using System.IO;

namespace ServiceHost
{
    public class FileUploader : IFileUploader
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public FileUploader(IWebHostEnvironment webHostEnvironment) => _webHostEnvironment = webHostEnvironment;

        public string Upload(IFormFile file, string path)
        {
            if (file is null)
                return "";

            var directoryPath = $"{_webHostEnvironment.WebRootPath}//UploadedFiles//{path}";
            if (!Directory.Exists(directoryPath))
                Directory.CreateDirectory(directoryPath);

            var fileName = $"{DateTime.Now.ToFileName()}-{file.FileName}"; 
            var filePath = $"{directoryPath}//{fileName}";
            using var output = File.Create(filePath);
            file.CopyTo(output);
            return $"{path}/{fileName}";
        }
    }
}
