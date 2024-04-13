using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace Common.Application;

public class FileExtentionLimitationAttribute : ValidationAttribute, IClientModelValidator
{
    private readonly string[] _validExtentions;
    public FileExtentionLimitationAttribute(string[] validExtentions) => _validExtentions = validExtentions;

    public override bool IsValid(object? value)
    {
        if (value is not IFormFile file) return true;
        var fileExtention = Path.GetExtension(file.FileName);
        return _validExtentions.Contains(fileExtention);
    }

    public void AddValidation(ClientModelValidationContext context) => context.Attributes.Add("data-val-fileExtentionLimit", ErrorMessage!);
}
