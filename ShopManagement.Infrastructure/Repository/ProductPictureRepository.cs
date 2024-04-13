using Common.Application;
using Common.Infrastructure;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Application.Contracts.ProductPictureAggregate;
using ShopManagement.Domain.ProductPictureAggregate;
using System.Collections.Generic;
using System.Linq;

namespace ShopManagement.Infrastructure.EFCore.Repository;

public class ProductPictureRepository(ShopDbContext context) : BaseRepository<long, ProductPicture>(context), IProductPictureRepository
{
    public EditProductPicture GetDetails(long id) => context.ProductPictures.Select(pp => new EditProductPicture
    {
        Id = pp.Id,
        PictureAlt = pp.PictureAlt,
        PictureTitle = pp.PictureTitle,
        ProductId = pp.ProductId
    }).FirstOrDefault(epp => epp.Id == id);
    public ProductPicture GetProductPictureWithProductAndCategory(long id) => context.ProductPictures.Include(pp => pp.Product).ThenInclude(p => p.Category).FirstOrDefault(pp => pp.Id == id);

    public List<ProductPictureViewModel> Search(ProductPictureSearchModel searchModel)
    {
        var query = context.ProductPictures.Include(pp => pp.Product).Select(pp => new ProductPictureViewModel
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
        return [.. query.OrderByDescending(ppvm => ppvm.Id)];
    }
}