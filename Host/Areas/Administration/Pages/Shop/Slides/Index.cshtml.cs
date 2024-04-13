using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShopManagement.Application.Contracts.SlideAggregate;

namespace Host.Areas.Administration.Pages.Shop.Slides;

public class IndexModel(ISlideApplication slideApplication) : PageModel
{
    [TempData]
    public string Message { get; set; } = string.Empty;

    public List<SlideViewModel> Slides { get; set; } = [];

    public void OnGet() => Slides = slideApplication.GetSlides();

    public IActionResult OnGetCreate()
    {
        var command = new CreateSlide();
        return Partial("./Create", command);
    }

    public JsonResult OnPostCreate(CreateSlide command)
    {
        var result = slideApplication.Create(command);
        Message = result.Message;
        return new JsonResult(result);
    }

    public IActionResult OnGetEdit(long id)
    {
        var slide = slideApplication.GetDetails(id);
        return Partial("./Edit", slide);
    }

    public JsonResult OnPostEdit(EditSlide command)
    {
        var result = slideApplication.Edit(command);
        Message = result.Message;
        return new JsonResult(result);
    }

    public IActionResult OnGetRemove(long id)
    {
        var result = slideApplication.Remove(id);
        Message = result.Message;
        return RedirectToPage("./Index");
    }

    public IActionResult OnGetRestore(long id)
    {
        var result = slideApplication.Restore(id);
        Message = result.Message;
        return RedirectToPage("./Index");
    }
}