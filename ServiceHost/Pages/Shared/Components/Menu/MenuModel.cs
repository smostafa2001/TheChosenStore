using LampShadeQuery.Contracts.ArticleCategoryAggregate;
using LampShadeQuery.Contracts.ProductCategoryAggregate;
using System.Collections.Generic;

namespace ServiceHost.Pages.Shared.Components.Menu
{
    public class MenuModel
    {
        public List<ArticleCategoryQueryModel> ArticleCategories { get; set; }
        public List<ProductCategoryQueryModel> ProductCategories { get; set; }
    }
}
