using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;

namespace ShopManagement.Domain.Shared
{
    public class FileExtentionLimitationAttribute : ValidationAttribute, IClientModelValidator
    {
        private readonly string[] _validExtensions;

        public FileExtentionLimitationAttribute(string[] validExtensions) => _validExtensions = validExtensions;
        public override bool IsValid(object value)
        {
            if (value is not IFormFile file)
                return true;
            var fileExtention = Path.GetExtension(file.FileName);
            return _validExtensions.Contains(fileExtention);
        }

        public void AddValidation(ClientModelValidationContext context) => context.Attributes.Add("data-val-fileExtentionLimit", ErrorMessage);
    }
}
