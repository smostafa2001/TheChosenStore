using DiscountManagement.Domain.CustomerDiscountAggregate;
using DiscountManagement.Domain.Shared;
using DiscountManagement.Infrastructure.EFCore.Shared;
using ShopManagement.Infrastructure.EFCore;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace DiscountManagement.Infrastructure.EFCore.Repository
{
    public class CustomerDiscountRepository : BaseRepository<long, CustomerDiscount>, ICustomerDiscountRepository
    {
        private readonly DiscountDbContext _dicountContext;
        private readonly ShopContext _shopContext;

        public CustomerDiscountRepository(DiscountDbContext context, ShopContext shopContext) : base(context)
        {
            _dicountContext = context;
            _shopContext = shopContext;
        }

        public EditCustomerDiscount GetDetails(long id) => _dicountContext.CustomerDiscounts.Select(cd => new EditCustomerDiscount
        {
            Id = cd.Id,
            DiscountRate = cd.DiscountRate,
            StartDate = cd.StartDate.ToString(CultureInfo.InvariantCulture),
            EndDate = cd.EndDate.ToString(CultureInfo.InvariantCulture),
            ProductId = cd.ProductId,
            Reason = cd.Reason
        }).FirstOrDefault(cd => cd.Id == id);

        public List<CustomerDiscountViewModel> Search(CustomerDiscountSearchModel searchModel)
        {
            var products = _shopContext.Products.Select(p => new { p.Id, p.Name }).ToList();
            var query = _dicountContext.CustomerDiscounts.Select(cd => new CustomerDiscountViewModel
            {
                Id = cd.Id,
                DiscountRate = cd.DiscountRate,
                StartDate = cd.StartDate,
                StartDateShamsi = cd.StartDate.ToFarsi(),
                EndDate = cd.EndDate,
                EndDateShamsi = cd.EndDate.ToFarsi(),
                Reason = cd.Reason,
                ProductId = cd.ProductId,
                CreationDate = cd.CreationDate.ToFarsi()
            });

            if (searchModel.ProductId > 0)
                query = query.Where(cd => cd.ProductId == searchModel.ProductId);

            if (!string.IsNullOrWhiteSpace(searchModel.StartDate))
                query = query.Where(cd => cd.StartDate > searchModel.StartDate.ToGeorgianDateTime());

            if (!string.IsNullOrWhiteSpace(searchModel.StartDate))
                query = query.Where(cd => cd.EndDate < searchModel.EndDate.ToGeorgianDateTime());

            var discounts = query.OrderByDescending(cd => cd.Id).ToList();
            discounts.ForEach(d => d.Product = products.FirstOrDefault(p => p.Id == d.ProductId)?.Name);
            return discounts;
        }
    }
}