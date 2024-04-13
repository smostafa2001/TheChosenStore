using Microsoft.AspNetCore.Mvc;

namespace Host.ViewComponents;

public class FooterViewComponent : ViewComponent
{
    public IViewComponentResult Invoke() => View();
}
