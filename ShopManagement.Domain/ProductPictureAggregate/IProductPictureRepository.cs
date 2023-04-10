using ShopManagement.Domain.Shared;
using System.Collections.Generic;

namespace ShopManagement.Domain.ProductPictureAggregate
{
    public interface IProductPictureRepository : IRepository<long, ProductPicture>
    {
        EditProductPicture GetDetails(long id);
        ProductPicture GetProductPictureWithProductAndCategory(long id);
        List<ProductPictureViewModel> Search(ProductPictureSearchModel searchModel);
    }
}