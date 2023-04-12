using Microsoft.AspNetCore.Http;

namespace Framework.Application
{
    public interface IFileUploader
    {
        string Upload(IFormFile file, string path);
    }
}
