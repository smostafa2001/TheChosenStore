using ShopManagement.Domain.Shared;
using System.Collections.Generic;

namespace ShopManagement.Domain.SlideAggregate
{
    public interface ISlideRepository : IRepository<long, Slide>
    {
        EditSlide GetDetails(long id);
        List<SlideViewModel> GetSlides();
    }
}
