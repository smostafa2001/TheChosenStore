using Common.Application;
using Common.Infrastructure;
using ShopManagement.Application.Contracts.SlideAggregate;
using ShopManagement.Domain.SlideAggregate;
using System.Collections.Generic;
using System.Linq;

namespace ShopManagement.Infrastructure.EFCore.Repository;

public class SlideRepository(ShopDbContext context) : BaseRepository<long, Slide>(context), ISlideRepository
{
    public EditSlide GetDetails(long id) => context.Slides.Select(s => new EditSlide
    {
        Id = s.Id,
        BtnText = s.BtnText,
        Heading = s.Heading,
        PictureAlt = s.PictureAlt,
        PictureTitle = s.PictureTitle,
        Text = s.Text,
        Title = s.Title,
        Link = s.Link
    }).FirstOrDefault(s => s.Id == id);

    public List<SlideViewModel> GetSlides() => [.. context.Slides.Select(s => new SlideViewModel
    {
        Id = s.Id,
        Heading = s.Heading,
        Picture = s.Picture,
        Title = s.Title,
        IsRemoved = s.IsRemoved,
        CreationDate = s.CreationDate.ToFarsi()
    }).OrderByDescending(s => s.Id)];
}