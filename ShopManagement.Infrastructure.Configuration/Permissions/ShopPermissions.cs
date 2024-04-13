namespace ShopManagement.Infrastructure.Configuration.Permissions;

public static class ShopPermissions
{
    #region ProductPermissions
    public const int ListProducts = 10;
    public const int SearchProducts = 11;
    public const int CreateProduct = 12;
    public const int EditProduct = 13;
    #endregion

    #region ProductCategoryPermissions
    public const int SearchProductCategories = 20;
    public const int ListProductCategories = 21;
    public const int CreateProductCategory = 22;
    public const int EditProductCategory = 23;
    #endregion
}
