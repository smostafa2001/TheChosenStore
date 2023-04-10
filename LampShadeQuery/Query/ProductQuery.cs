using DiscountManagement.Infrastructure.EFCore;
using InventoryManagement.Infrastructure.EFCore;
using LampShadeQuery.Contracts.ProductAggregate;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Domain.Shared;
using ShopManagement.Infrastructure.EFCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LampShadeQuery.Query
{
    public class ProductQuery : IProductQuery
    {
        private readonly ShopContext _shopContext;
        private readonly InventoryDbContext _inventoryContext;
        private readonly DiscountDbContext _discountContext;

        public ProductQuery(ShopContext shopContext, InventoryDbContext inventoryContext, DiscountDbContext discountContext)
        {
            _shopContext = shopContext;
            _inventoryContext = inventoryContext;
            _discountContext = discountContext;
        }

        public ProductQueryModel GetDetails(string slug)
        {
            var inventory = _inventoryContext.Inventory.Select(i => new { i.ProductId, i.UnitPrice, i.IsInStock }).ToList();

            var discounts = _discountContext.CustomerDiscounts
                .Where(cd => cd.StartDate < DateTime.Now && cd.EndDate > DateTime.Now)
                .Select(cd => new { cd.DiscountRate, cd.ProductId, cd.EndDate })
                .ToList();

            var product = _shopContext.Products.Include(p => p.Category)
                .Select(p => new ProductQueryModel
                {
                    Id = p.Id,
                    Category = p.Category.Name,
                    Name = p.Name,
                    Picture = p.Picture,
                    PictureAlt = p.PictureAlt,
                    PictureTitle = p.PictureTitle,
                    Slug = p.Slug,
                    CategorySlug = p.Category.Slug,
                    Code = p.Code,
                    Description = p.Description,
                    Keywords = p.Keywords,
                    MetaDescription = p.MetaDescription,
                    ShortDescription = p.ShortDescription
                }).FirstOrDefault(p => p.Slug == slug);

            if (product is null)
                return new ProductQueryModel();

            var productInventory = inventory.FirstOrDefault(i => i.ProductId == product.Id);
            if (productInventory is not null)
            {
                product.IsInStock = productInventory.IsInStock;
                var price = productInventory.UnitPrice;
                product.Price = price.ToMoney();

                var discount = discounts.FirstOrDefault(d => d.ProductId == product.Id);
                if (discount is not null)
                {
                    product.DiscountRate = discount.DiscountRate;
                    product.HasDiscount = product.DiscountRate > 0;
                    product.DiscountExpireDate = discount.EndDate.ToDiscountFormat();
                    var discountAmount = Math.Round(price * product.DiscountRate / 100);
                    product.PriceWithDiscount = (price - discountAmount).ToMoney();
                }
            }

            return product;
        }

        public List<ProductQueryModel> GetLatestArrivals()
        {
            var inventory = _inventoryContext.Inventory.Select(i => new { i.ProductId, i.UnitPrice }).ToList();

            var discounts = _discountContext.CustomerDiscounts
                .Where(cd => cd.StartDate < DateTime.Now && cd.EndDate > DateTime.Now)
                .Select(cd => new { cd.DiscountRate, cd.ProductId })
                .ToList();

            var products = _shopContext.Products.Include(p => p.Category)
                .Select(p => new ProductQueryModel
                {
                    Id = p.Id,
                    Category = p.Category.Name,
                    Name = p.Name,
                    Picture = p.Picture,
                    PictureAlt = p.PictureAlt,
                    PictureTitle = p.PictureTitle,
                    Slug = p.Slug,
                }).AsNoTracking().OrderByDescending(p => p.Id).Take(6).ToList();

            foreach (var product in products)
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

            return products;
        }

        public List<ProductQueryModel> Search(string value)
        {
            var inventory = _inventoryContext.Inventory.Select(i => new { i.ProductId, i.UnitPrice }).ToList();
            var discounts = _discountContext.CustomerDiscounts
                .Where(cd => cd.StartDate < DateTime.Now && cd.EndDate > DateTime.Now)
                .Select(cd => new { cd.DiscountRate, cd.ProductId, cd.EndDate })
                .ToList();
            var query = _shopContext.Products.Include(p => p.Category).Select(p => new ProductQueryModel
            {
                Id = p.Id,
                Category = p.Category.Name,
                Name = p.Name,
                Picture = p.Picture,
                PictureAlt = p.PictureAlt,
                PictureTitle = p.PictureTitle,
                Slug = p.Slug
            }).AsNoTracking();

            if (!string.IsNullOrWhiteSpace(value))
                query = query.Where(p => p.Name.Contains(value));

            var products = query.OrderByDescending(p => p.Id).ToList();
            foreach (var product in products)
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

            return products;
        }
    }
}