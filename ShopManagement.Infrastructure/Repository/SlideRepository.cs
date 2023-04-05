using ShopManagement.Domain.SlideAggregate;
using ShopManagement.Infrastructure.EfCore.Shared;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace ShopManagement.Infrastructure.EfCore.Repository
{
    public class SlideRepository : BaseRepository<long, Slide>, ISlideRepository
    {
        private readonly ShopContext _context;

        public SlideRepository(ShopContext context) : base(context) => _context = context;

        public EditSlide GetDetails(long id) => _context.Slides.Select(s => new EditSlide
        {
            Id = s.Id,
            BtnText = s.BtnText,
            Heading = s.Heading,
            Picture = s.Picture,
            PictureAlt = s.PictureAlt,
            PictureTitle = s.PictureTitle,
            Text = s.Text,
            Title = s.Title
        }).FirstOrDefault(s => s.Id == id);
        public List<SlideViewModel> GetSlides() => _context.Slides.Select(s => new SlideViewModel
        {
            Id = s.Id,
            Heading = s.Heading,
            Picture = s.Picture,
            Title = s.Title,
            IsRemoved = s.IsRemoved,
            CreationDate = s.CreationDate.ToString(CultureInfo.InvariantCulture)
        }).OrderByDescending(s => s.Id).ToList();
    }
}
