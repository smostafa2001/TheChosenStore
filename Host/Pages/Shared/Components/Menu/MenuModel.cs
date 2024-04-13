using DecorativeStoreQuery.Contracts.ArticleCategoryAggregate;
using DecorativeStoreQuery.Contracts.ProductCategoryAggregate;

namespace Host.Pages.Shared.Components.Menu;

public class MenuModel
{
    public List<ArticleCategoryQueryModel>? ArticleCategories { get; set; }
    public List<ProductCategoryQueryModel>? ProductCategories { get; set; }
}
