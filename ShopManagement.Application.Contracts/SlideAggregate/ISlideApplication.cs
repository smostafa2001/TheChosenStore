using ShopManagement.Application.Contracts.Shared;
using ShopManagement.Domain.SlideAggregate;
using System.Collections.Generic;

namespace ShopManagement.Application.Contracts.SlideAggregate
{
    public interface ISlideApplication
    {
        OperationResult Create(CreateSlide command);

        OperationResult Edit(EditSlide command);

        OperationResult Remove(long id);

        OperationResult Restore(long id);

        EditSlide GetDetails(long id);

        List<SlideViewModel> GetSlides();
    }
}