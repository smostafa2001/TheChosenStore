using Common.Application;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace ShopManagement.Application.Contracts.SlideAggregate;

public class CreateSlide
{
    public IFormFile Picture { get; set; }

    [Required(ErrorMessage = ValidationMessages.IsRequired)]
    public string PictureAlt { get; set; }

    [Required(ErrorMessage = ValidationMessages.IsRequired)]
    public string PictureTitle { get; set; }

    [Required(ErrorMessage = ValidationMessages.IsRequired)]
    public string Heading { get; set; }

    [Required(ErrorMessage = ValidationMessages.IsRequired)]
    public string Title { get; set; }

    [Required(ErrorMessage = ValidationMessages.IsRequired)]
    public string Text { get; set; }

    [Required(ErrorMessage = ValidationMessages.IsRequired)]
    public string BtnText { get; set; }

    [Required(ErrorMessage = ValidationMessages.IsRequired)]
    public string Link { get; set; }
}