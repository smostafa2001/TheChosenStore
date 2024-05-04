using TheChosenStoreQuery.Contracts.ArticleCategoryAggregate;
using TheChosenStoreQuery.Contracts.ProductCategoryAggregate;

namespace Host.Pages.Shared.Components.Menu;

public class MenuModel
{
    public List<ArticleCategoryQueryModel>? ArticleCategories { get; set; }
    public List<ProductCategoryQueryModel>? ProductCategories { get; set; }
}
