using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShopManagement.Application.Contracts.SlideAggregate;
using System.Collections.Generic;

namespace ServiceHost.Areas.Administration.Pages.Shop.Slides
{
    public class IndexModel : PageModel
    {
        private readonly ISlideApplication _slideApplication;
        [TempData]
        public string Message { get; set; }

        public List<SlideViewModel> Slides { get; set; }

        public IndexModel(ISlideApplication slideApplication) => _slideApplication = slideApplication;

        public void OnGet() => Slides = _slideApplication.GetSlides();

        public IActionResult OnGetCreate()
        {
            var command = new CreateSlide();
            return Partial("./Create", command);
        }

        public JsonResult OnPostCreate(CreateSlide command)
        {
            var result = _slideApplication.Create(command);
            Message = result.Message;
            return new JsonResult(result);
        }

        public IActionResult OnGetEdit(long id)
        {
            var slide = _slideApplication.GetDetails(id);
            return Partial("./Edit", slide);
        }

        public JsonResult OnPostEdit(EditSlide command)
        {
            var result = _slideApplication.Edit(command);
            Message = result.Message;
            return new JsonResult(result);
        }

        public IActionResult OnGetRemove(long id)
        {
            var result = _slideApplication.Remove(id);
            Message = result.Message;
            return RedirectToPage("./Index");
        }

        public IActionResult OnGetRestore(long id)
        {
            var result = _slideApplication.Restore(id);
            Message = result.Message;
            return RedirectToPage("./Index");
        }
    }
}