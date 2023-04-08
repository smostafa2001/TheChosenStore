using ShopManagement.Domain.SlideAggregate;
using System.Collections.Generic;
using System.Linq;

namespace ShopManagement.Infrastructure.EFCore.Query
{
    public class SlideQuery : ISlideQuery
    {
        private readonly ShopContext _shopContext;

        public SlideQuery(ShopContext shopContext) => _shopContext = shopContext;

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