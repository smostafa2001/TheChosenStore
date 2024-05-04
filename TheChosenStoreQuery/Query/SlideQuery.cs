using ShopManagement.Infrastructure.EFCore;
using System.Collections.Generic;
using System.Linq;
using TheChosenStoreQuery.Contracts.SlideAggregate;

namespace TheChosenStoreQuery.Query;

public class SlideQuery(ShopDbContext shopContext) : ISlideQuery
{
    public List<SlideQueryModel> GetSlides() => [.. shopContext.Slides.Where(s => s.IsRemoved == false).Select(s => new SlideQueryModel
    {
        Picture = s.Picture,
        PictureAlt = s.PictureAlt,
        PictureTitle = s.PictureTitle,
        Title = s.Title,
        Heading = s.Heading,
        Text = s.Text,
        BtnText = s.BtnText,
        Link = s.Link
    })];
}