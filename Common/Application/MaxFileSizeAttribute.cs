using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace Common.Application;

public class MaxFileSizeAttribute(int maxFileSize) : ValidationAttribute, IClientModelValidator
{
    public override bool IsValid(object? value) => value is not IFormFile file || file.Length <= maxFileSize;

    public void AddValidation(ClientModelValidationContext context)
    {
        context.Attributes.Add("data-val", "true");
        context.Attributes.Add("data-val-maxFileSize", ErrorMessage!);
    }
}
