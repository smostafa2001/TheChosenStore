using System.Collections.Generic;

namespace TheChosenStoreQuery.Contracts.SlideAggregate;

public interface ISlideQuery
{
    List<SlideQueryModel> GetSlides();
}