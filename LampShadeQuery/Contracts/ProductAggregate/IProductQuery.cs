using System.Collections.Generic;

namespace LampShadeQuery.Contracts.ProductAggregate
{
    public interface IProductQuery
    {
        List<ProductQueryModel> GetLatestArrivals();
        List<ProductQueryModel> Search(string value);
    }
}
