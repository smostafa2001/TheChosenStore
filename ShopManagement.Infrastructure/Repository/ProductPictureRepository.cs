using Microsoft.EntityFrameworkCore;
using ShopManagement.Domain.ProductPictureAggregate;
using ShopManagement.Domain.Shared;
using ShopManagement.Infrastructure.EFCore.Shared;
using System.Collections.Generic;
using System.Linq;

namespace ShopManagement.Infrastructure.EFCore.Repository
{
    public class ProductPictureRepository : BaseRepository<long, ProductPicture>, IProductPictureRepository
    {
        private readonly ShopContext _context;

        public ProductPictureRepository(ShopContext context) : base(context) => _context = context;

        public EditProductPicture GetDetails(long id) => _context.ProductPictures.Select(pp => new EditProductPicture
        {
            Id = pp.Id,
            PictureAlt = pp.PictureAlt,
            PictureTitle = pp.PictureTitle,
            ProductId = pp.ProductId
        }).FirstOrDefault(epp => epp.Id == id);
        public ProductPicture GetProductPictureWithProductAndCategory(long id) => _context.ProductPictures.Include(pp => pp.Product).ThenInclude(p => p.Category).FirstOrDefault(pp => pp.Id == id);

        public List<ProductPictureViewModel> Search(ProductPictureSearchModel searchModel)
        {
            var query = _context.ProductPictures.Include(pp => pp.Product).Select(pp => new ProductPictureViewModel
            {
                Id = pp.Id,
                Product = pp.Product.Name,
                CreationDate = pp.CreationDate.ToFarsi(),
                Picture = pp.Picture,
                ProductId = pp.ProductId,
                IsRemoved = pp.IsRemoved
            });

            if (searchModel.ProductId != 0)
                query = query.Where(pp => pp.ProductId == searchModel.ProductId);
            return query.OrderByDescending(ppvm => ppvm.Id).ToList();
        }
    }
}