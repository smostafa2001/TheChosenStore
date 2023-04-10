using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopManagement.Application.Contracts.Shared
{
    public interface IFileUploader
    {
        string Upload(IFormFile file, string path);
    }
}
