using LampShadeQuery.Contracts.ProductCategoryAggregate;
using Microsoft.AspNetCore.Mvc;

namespace ServiceHost.ViewComponents
{
    public class MenuViewComponent : ViewComponent
    {
        private readonly IProductCategoryQuery _categoryQuery;

        public MenuViewComponent(IProductCategoryQuery categoryQuery) => _categoryQuery = categoryQuery;

        public IViewComponentResult Invoke() => View();
    }
}
