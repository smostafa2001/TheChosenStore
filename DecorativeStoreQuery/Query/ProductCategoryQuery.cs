using Common.Application;
using DecorativeStoreQuery.Contracts.ProductAggregate;
using DecorativeStoreQuery.Contracts.ProductCategoryAggregate;
using DiscountManagement.Infrastructure.EFCore;
using InventoryManagement.Infrastructure.EFCore;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Domain.ProductAggregate;
using ShopManagement.Infrastructure.EFCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DecorativeStoreQuery.Query;

public class ProductCategoryQuery(ShopDbContext shopContext, InventoryDbContext inventoryContext, DiscountDbContext discountContext) : IProductCategoryQuery
{
    public List<ProductCategoryQueryModel> GetProductCategories() => [.. shopContext.ProductCategories.Select(pc => new ProductCategoryQueryModel
    {
        Id = pc.Id,
        Name = pc.Name,
        Picture = pc.Picture,
        PictureAlt = pc.PictureAlt,
        PictureTitle = pc.PictureTitle,
        Slug = pc.Slug
    })];
    public List<ProductCategoryQueryModel> GetProductCategoriesWithProducts()
    {
        var inventory = inventoryContext.Inventory.Select(i => new { i.ProductId, i.UnitPrice }).ToList();
        var discounts = discountContext.CustomerDiscounts
            .Where(cd => cd.StartDate < DateTime.Now && cd.EndDate > DateTime.Now)
            .Select(cd => new { cd.DiscountRate, cd.ProductId })
            .ToList();

        var categories = shopContext.ProductCategories.Include(pc => pc.Products).ThenInclude(p => p.Category).Select(pc => new ProductCategoryQueryModel
        {
            Id = pc.Id,
            Name = pc.Name,
            Products = MapProducts(pc.Products),

        }).ToList();

        foreach (var category in categories)
            foreach (var product in category.Products)
            {
                var productInventory = inventory.FirstOrDefault(i => i.ProductId == product.Id);
                if (productInventory is not null)
                {
                    var price = productInventory.UnitPrice;
                    product.Price = price.ToMoney();

                    var discount = discounts.FirstOrDefault(d => d.ProductId == product.Id);
                    if (discount is not null)
                    {
                        product.DiscountRate = discount.DiscountRate;
                        product.HasDiscount = product.DiscountRate > 0;
                        var discountAmount = Math.Round(price * product.DiscountRate / 100);
                        product.PriceWithDiscount = (price - discountAmount).ToMoney();
                    }
                }
            }

        return categories;
    }

    private static List<ProductQueryModel> MapProducts(List<Product> products) => products.Select(p => new ProductQueryModel
    {
        Id = p.Id,
        Category = p.Category.Name,
        Name = p.Name,
        Picture = p.Picture,
        PictureAlt = p.PictureAlt,
        PictureTitle = p.PictureTitle,
        Slug = p.Slug,
    }).ToList();
    public ProductCategoryQueryModel GetProductCategoryWithProducts(string slug)
    {
        var inventory = inventoryContext.Inventory.Select(i => new { i.ProductId, i.UnitPrice }).ToList();
        var discounts = discountContext.CustomerDiscounts
            .Where(cd => cd.StartDate < DateTime.Now && cd.EndDate > DateTime.Now)
            .Select(cd => new { cd.DiscountRate, cd.ProductId, cd.EndDate })
            .ToList();

        var category = shopContext.ProductCategories.Include(pc => pc.Products).ThenInclude(p => p.Category).Select(pc => new ProductCategoryQueryModel
        {
            Id = pc.Id,
            Name = pc.Name,
            Description = pc.Description,
            MetaDescription = pc.MetaDescription,
            Keywords = pc.Keywords,
            Products = MapProducts(pc.Products),
            Slug = pc.Slug
        }).FirstOrDefault(pc => pc.Slug == slug);

        foreach (var product in category?.Products!)
        {
            var productInventory = inventory.FirstOrDefault(i => i.ProductId == product.Id);
            if (productInventory is not null)
            {
                var price = productInventory.UnitPrice;
                product.Price = price.ToMoney();

                var discount = discounts.FirstOrDefault(d => d.ProductId == product.Id);
                if (discount is not null)
                {
                    product.DiscountRate = discount.DiscountRate;
                    product.DiscountExpireDate = discount.EndDate.ToDiscountFormat();
                    product.HasDiscount = product.DiscountRate > 0;
                    var discountAmount = Math.Round(price * product.DiscountRate / 100);
                    product.PriceWithDiscount = (price - discountAmount).ToMoney();
                }
            }
        }

        return category;
    }
}