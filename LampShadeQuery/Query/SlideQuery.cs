using LampShadeQuery.Contracts.SlideAggregate;
using ShopManagement.Infrastructure.EFCore;
using System.Collections.Generic;
using System.Linq;

namespace LampShadeQuery.Query
{
    public class SlideQuery : ISlideQuery
    {
        private readonly ShopDbContext _shopContext;

        public SlideQuery(ShopDbContext shopContext) => _shopContext = shopContext;

        public List<SlideQueryModel> GetSlides() => _shopContext.Slides.Where(s => s.IsRemoved == false).Select(s => new SlideQueryModel
        {
            Picture = s.Picture,
            PictureAlt = s.PictureAlt,
            PictureTitle = s.PictureTitle,
            Title = s.Title,
            Heading = s.Heading,
            Text = s.Text,
            BtnText = s.BtnText,
            Link = s.Link
        }).ToList();
    }
}