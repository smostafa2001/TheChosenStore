using Microsoft.AspNetCore.Mvc;
using TheChosenStoreQuery.Contracts.SlideAggregate;

namespace Host.ViewComponents;

public class SlideViewComponent(ISlideQuery slideQuery) : ViewComponent
{
    public IViewComponentResult Invoke()
    {
        var slides = slideQuery.GetSlides();
        return View(slides);
    }
}