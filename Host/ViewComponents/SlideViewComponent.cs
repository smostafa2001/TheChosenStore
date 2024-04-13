using DecorativeStoreQuery.Contracts.SlideAggregate;
using Microsoft.AspNetCore.Mvc;

namespace Host.ViewComponents;

public class SlideViewComponent(ISlideQuery slideQuery) : ViewComponent
{
    public IViewComponentResult Invoke()
    {
        var slides = slideQuery.GetSlides();
        return View(slides);
    }
}