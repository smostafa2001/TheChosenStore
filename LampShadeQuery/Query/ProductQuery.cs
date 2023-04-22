using CommentManagement.Infrastructure.EFCore;
using DiscountManagement.Infrastructure.EFCore;
using Framework.Application;
using InventoryManagement.Infrastructure.EFCore;
using LampShadeQuery.Contracts.CommentAggregate;
using LampShadeQuery.Contracts.ProductAggregate;
using LampShadeQuery.Contracts.ProductPictureAggregate;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Application.Contracts.OrderAggregate;
using ShopManagement.Domain.ProductPictureAggregate;
using ShopManagement.Infrastructure.EFCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LampShadeQuery.Query
{
    public class ProductQuery : IProductQuery
    {
        private readonly ShopDbContext _shopContext;
        private readonly InventoryDbContext _inventoryContext;
        private readonly DiscountDbContext _discountContext;
        private readonly CommentDbContext _commentContext;

        public ProductQuery(ShopDbContext shopContext, InventoryDbContext inventoryContext, DiscountDbContext discountContext, CommentDbContext commentContext)
        {
            _shopContext = shopContext;
            _inventoryContext = inventoryContext;
            _discountContext = discountContext;
            _commentContext = commentContext;
        }

        public ProductQueryModel GetDetails(string slug)
        {
            var inventory = _inventoryContext.Inventory.Select(i => new { i.ProductId, i.UnitPrice, i.IsInStock }).ToList();
            var discounts = _discountContext.CustomerDiscounts
                .Where(cd => cd.StartDate < DateTime.Now && cd.EndDate > DateTime.Now)
                .Select(cd => new { cd.DiscountRate, cd.ProductId, cd.EndDate })
                .ToList();

            ProductQueryModel product = _shopContext.Products.Include(p => p.Category).Include(p => p.ProductPictures)
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
                    ShortDescription = p.ShortDescription,
                    Pictures = MapProductPictures(p.ProductPictures)
                }).FirstOrDefault(p => p.Slug == slug);

            if (product is null)
                return new ProductQueryModel();

            var productInventory = inventory.FirstOrDefault(i => i.ProductId == product.Id);
            if (productInventory is not null)
            {
                product.IsInStock = productInventory.IsInStock;
                double price = productInventory.UnitPrice;
                product.DoublePrice = price;
                product.Price = price.ToMoney();

                var discount = discounts.FirstOrDefault(d => d.ProductId == product.Id);
                if (discount is not null)
                {
                    product.DiscountRate = discount.DiscountRate;
                    product.HasDiscount = product.DiscountRate > 0;
                    product.DiscountExpireDate = discount.EndDate.ToDiscountFormat();
                    double discountAmount = Math.Round(price * product.DiscountRate / 100);
                    product.PriceWithDiscount = (price - discountAmount).ToMoney();
                }
            }

            product.Comments = _commentContext.Comments.Where(c => c.Type == CommentType.Product && !c.IsCanceled && c.IsConfirmed && c.OwnerRecordId == product.Id).Select(c => new CommentQueryModel
            {
                Id = c.Id,
                Message = c.Message,
                Name = c.Name,
                CreationDate = c.CreationDate.ToFarsi()
            }).OrderByDescending(c => c.Id).ToList();
            return product;
        }

        private static List<ProductPictureQueryModel> MapProductPictures(List<ProductPicture> productPictures) => productPictures.Select(pp => new ProductPictureQueryModel
        {
            IsRemoved = pp.IsRemoved,
            Picture = pp.Picture,
            PictureAlt = pp.PictureAlt,
            PictureTitle = pp.PictureTitle,
            ProductId = pp.ProductId
        }).Where(pp => !pp.IsRemoved).ToList();

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

            foreach (ProductQueryModel product in products)
            {
                var productInventory = inventory.FirstOrDefault(i => i.ProductId == product.Id);
                if (productInventory is not null)
                {
                    double price = productInventory.UnitPrice;
                    product.Price = price.ToMoney();

                    var discount = discounts.FirstOrDefault(d => d.ProductId == product.Id);
                    if (discount is not null)
                    {
                        product.DiscountRate = discount.DiscountRate;
                        product.HasDiscount = product.DiscountRate > 0;
                        double discountAmount = Math.Round(price * product.DiscountRate / 100);
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
            IQueryable<ProductQueryModel> query = _shopContext.Products.Include(p => p.Category).Select(p => new ProductQueryModel
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
            foreach (ProductQueryModel product in products)
            {
                var productInventory = inventory.FirstOrDefault(i => i.ProductId == product.Id);
                if (productInventory is not null)
                {
                    double price = productInventory.UnitPrice;
                    product.Price = price.ToMoney();

                    var discount = discounts.FirstOrDefault(d => d.ProductId == product.Id);
                    if (discount is not null)
                    {
                        product.DiscountRate = discount.DiscountRate;
                        product.DiscountExpireDate = discount.EndDate.ToDiscountFormat();
                        product.HasDiscount = product.DiscountRate > 0;
                        double discountAmount = Math.Round(price * product.DiscountRate / 100);
                        product.PriceWithDiscount = (price - discountAmount).ToMoney();
                    }
                }
            }

            return products;
        }

        public List<CartItem> CheckInventoryStatus(List<CartItem> cartItems)
        {
            var inventory = _inventoryContext.Inventory.ToList();
            if (cartItems is not null)
            {
                foreach (CartItem item in cartItems.Where(item => inventory.Any(i => i.ProductId == item.Id && i.IsInStock)))
                {
                    var itemInventory = inventory.Find(i => i.ProductId == item.Id);
                    item.IsInStock = itemInventory.CalculateCurrentStock() >= item.Count;
                }
            }

            return cartItems;
        }
    }
}