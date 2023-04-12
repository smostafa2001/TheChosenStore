using Framework.Application;
using Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Application.Contracts.ProductAggregate;
using ShopManagement.Domain.ProductAggregate;
using System.Collections.Generic;
using System.Linq;

namespace ShopManagement.Infrastructure.EFCore.Repository
{
    public class ProductRepository : BaseRepository<long, Product>, IProductRepository
    {
        private readonly ShopDbContext _shopContext;

        public ProductRepository(ShopDbContext context) : base(context) => _shopContext = context;

        public EditProduct GetDetails(long id) => _shopContext.Products.Select(x => new EditProduct
        {
            Id = x.Id,
            Name = x.Name,
            Code = x.Code,
            Slug = x.Slug,
            CategoryId = x.CategoryId,
            Description = x.Description,
            Keywords = x.Keywords,
            MetaDescription = x.MetaDescription,
            PictureAlt = x.PictureAlt,
            PictureTitle = x.PictureTitle,
            ShortDescription = x.ShortDescription
        }).FirstOrDefault(x => x.Id == id);

        public List<ProductViewModel> GetProducts() => _shopContext.Products.Select(x => new ProductViewModel
        {
            Id = x.Id,
            Name = x.Name
        }).ToList();
        public Product GetProductWithCategory(long id) => _shopContext.Products.Include(p => p.Category).FirstOrDefault(p => p.Id == id);

        public List<ProductViewModel> Search(ProductSearchModel searchModel)
        {
            var query = _shopContext.Products.Include(x => x.Category)
                .Select(x => new ProductViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Category = x.Category.Name,
                    CategoryId = x.CategoryId,
                    Code = x.Code,
                    Picture = x.Picture,
                    CreationDate = x.CreationDate.ToFarsi(),
                });

            if (!string.IsNullOrWhiteSpace(searchModel.Name))
                query = query.Where(x => x.Name.Contains(searchModel.Name));

            if (!string.IsNullOrWhiteSpace(searchModel.Code))
                query = query.Where(x => x.Code.Contains(searchModel.Code));

            if (searchModel.CategoryId != 0)
                query = query.Where(x => x.CategoryId == searchModel.CategoryId);

            return query.OrderByDescending(x => x.Id).ToList();
        }
    }
}