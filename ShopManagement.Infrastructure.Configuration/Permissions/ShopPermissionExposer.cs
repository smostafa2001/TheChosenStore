using Framework.Infrastructure;
using System.Collections.Generic;

namespace ShopManagement.Infrastructure.Configuration.Permissions
{
    internal class ShopPermissionExposer : IPermissionExposer
    {
        public Dictionary<string, List<PermissionDto>> Expose() => new Dictionary<string, List<PermissionDto>>
        {
            {
                "Product", new List<PermissionDto>
                {
                    new PermissionDto(ShopPermissions.ListProducts, "ListProducts"),
                    new PermissionDto(ShopPermissions.SearchProducts, "SearchProducts"),
                    new PermissionDto(ShopPermissions.CreateProduct, "CreateProduct"),
                    new PermissionDto(ShopPermissions.EditProduct, "EditProduct"),
                }
            },
            {
                "ProductCategory", new List<PermissionDto>
                {
                    new PermissionDto(ShopPermissions.SearchProductCategories, "SearchProductCategories"),
                    new PermissionDto(ShopPermissions.ListProductCategories, "ListProductCategories"),
                    new PermissionDto(ShopPermissions.CreateProductCategory, "CreateProductCategory"),
                    new PermissionDto(ShopPermissions.EditProductCategory, "EditProductCategory")

                }
            }
        };
    }
}
