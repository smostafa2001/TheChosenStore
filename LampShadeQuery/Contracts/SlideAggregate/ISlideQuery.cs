using System.Collections.Generic;

namespace LampShadeQuery.Contracts.SlideAggregate
{
    public interface ISlideQuery
    {
        List<SlideQueryModel> GetSlides();
    }
}