using System.Collections.Generic;

namespace ShopManagement.Domain.SlideAggregate
{
    public class SlideQueryModel : CreateSlide
    {
    }

    public interface ISlideQuery
    {
        List<SlideQueryModel> GetSlides();
    }
}