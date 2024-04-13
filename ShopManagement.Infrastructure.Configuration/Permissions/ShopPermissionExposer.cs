using Common.Infrastructure;
using System.Collections.Generic;

namespace ShopManagement.Infrastructure.Configuration.Permissions;

internal class ShopPermissionExposer : IPermissionExposer
{
    public Dictionary<string, List<PermissionDto>> Expose() => new()
    {
        {
            "Product", new List<PermissionDto>
            {
                new(ShopPermissions.ListProducts, "ListProducts"),
                new(ShopPermissions.SearchProducts, "SearchProducts"),
                new(ShopPermissions.CreateProduct, "CreateProduct"),
                new(ShopPermissions.EditProduct, "EditProduct"),
            }
        },
        {
            "ProductCategory", new List<PermissionDto>
            {
                new(ShopPermissions.SearchProductCategories, "SearchProductCategories"),
                new(ShopPermissions.ListProductCategories, "ListProductCategories"),
                new(ShopPermissions.CreateProductCategory, "CreateProductCategory"),
                new(ShopPermissions.EditProductCategory, "EditProductCategory")
            }
        }
    };
}
