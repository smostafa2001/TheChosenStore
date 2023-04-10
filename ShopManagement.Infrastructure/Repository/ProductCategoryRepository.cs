using ShopManagement.Domain.ProductCategoryAggregate;
using ShopManagement.Domain.Shared;
using ShopManagement.Infrastructure.EFCore.Shared;
using System.Collections.Generic;
using System.Linq;

namespace ShopManagement.Infrastructure.EFCore.Repository
{
    public class ProductCategoryRepository : BaseRepository<long, ProductCategory>, IProductCategoryRepository
    {
        private readonly ShopContext _context;

        public ProductCategoryRepository(ShopContext context) : base(context) => _context = context;

        public EditProductCategory GetDetails(long id) => _context.ProductCategories.Select(pc => new EditProductCategory()
        {
            Id = pc.Id,
            Description = pc.Description,
            Name = pc.Name,
            Keywords = pc.Keywords,
            MetaDescription = pc.MetaDescription,
            //Picture = pc.Picture,
            PictureAlt = pc.PictureAlt,
            PictureTitle = pc.PictureTitle,
            Slug = pc.Slug
        }).FirstOrDefault(x => x.Id == id);

        public List<ProductCategoryViewModel> GetProductCategories() => _context.ProductCategories.Select(pc => new ProductCategoryViewModel
        {
            Id = pc.Id,
            Name = pc.Name
        }).ToList();
        public string GetSlug(long id) => _context.ProductCategories.Select(pc=>new {pc.Id, pc.Slug }).FirstOrDefault(pc=>pc.Id == id).Slug;

        public List<ProductCategoryViewModel> Search(ProductCategorySearchModel searchModel)
        {
            var query = _context.ProductCategories.Select(pc => new ProductCategoryViewModel
            {
                Id = pc.Id,
                Picture = pc.Picture,
                Name = pc.Name,
                CreationDate = pc.CreationDate.ToFarsi()
            });

            if (!string.IsNullOrWhiteSpace(searchModel.Name))
                query = query.Where(x => x.Name.Contains(searchModel.Name));

            return query.OrderByDescending(x => x.Id).ToList();
        }
    }
}