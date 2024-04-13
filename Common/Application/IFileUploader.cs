using Microsoft.AspNetCore.Http;

namespace Common.Application;

public interface IFileUploader
{
    string Upload(IFormFile file, string path);
}
