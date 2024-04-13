using System.Collections.Generic;

namespace DecorativeStoreQuery.Contracts.SlideAggregate;

public interface ISlideQuery
{
    List<SlideQueryModel> GetSlides();
}