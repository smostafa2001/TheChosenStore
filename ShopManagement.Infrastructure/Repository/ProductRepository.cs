using Microsoft.EntityFrameworkCore;
using ShopManagement.Domain.ProductAggregate;
using ShopManagement.Infrastructure.EfCore.Shared;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace ShopManagement.Infrastructure.EfCore.Repository
{
    public class ProductRepository : BaseRepository<long, Product>, IProductRepository
    {
        private readonly ShopContext _context;

        public ProductRepository(ShopContext context) : base(context) => _context = context;

        public EditProduct GetDetails(long id) => _context.Products.Select(x => new EditProduct
        {
            Id = x.Id,
            Name = x.Name,
            Code = x.Code,
            Slug = x.Slug,
            CategoryId = x.CategoryId,
            Description = x.Description,
            UnitPrice = x.UnitPrice,
            Picture = x.Picture,
            Keywords = x.Keywords,
            MetaDescription = x.MetaDescription,
            PictureAlt = x.PictureAlt,
            PictureTitle = x.PictureTitle,
            ShortDescription = x.ShortDescription
        }).FirstOrDefault(x => x.Id == id);
        public List<ProductViewModel> GetProducts() => _context.Products.Select(x => new ProductViewModel
        {
            Id = x.Id,
            Name = x.Name
        }).ToList();
        public List<ProductViewModel> Search(ProductSearchModel searchModel)
        {
            var query = _context.Products.Include(x => x.Category)
                .Select(x => new ProductViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Category = x.Category.Name,
                    CategoryId = x.CategoryId,
                    Code = x.Code,
                    Picture = x.Picture,
                    UnitPrice = x.UnitPrice,
                    CreationDate = x.CreationDate.ToString(CultureInfo.InvariantCulture),
                    IsInStock = x.IsInStock
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

