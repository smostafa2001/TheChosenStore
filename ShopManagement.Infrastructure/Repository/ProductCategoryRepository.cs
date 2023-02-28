using _01.Framework.Infrastructure;
using ShopManagement.Domain.ProductCategoryAggregate;
using System.Collections.Generic;
using System.Linq;

namespace ShopManagement.Infrastructure.EfCore.Repository
{
    public class ProductCategoryRepository : BaseRepository<long, ProductCategory>, IProductCategoryRepository
    {
        private readonly ShopContext _context;

        public ProductCategoryRepository(ShopContext context) : base(context)
        {
            _context = context;
        }

        public ProductCategory GetDetails(long id)
        {
            return _context.ProductCategories.FirstOrDefault(x => x.Id == id);
        }

        public List<ProductCategory> Search(string name)
        {
            var query = _context.ProductCategories.AsQueryable();
            if (!string.IsNullOrWhiteSpace(name))
                query = query.Where(x => x.Name.Contains(name));
            return query.OrderByDescending(x => x.Id).ToList();
        }
    }
}
