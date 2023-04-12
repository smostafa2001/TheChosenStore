using DiscountManagement.Application.Contracts.ColleagueDiscountAggregate;
using DiscountManagement.Domain.ColleagueDiscountAggregate;
using Framework.Application;
using Framework.Infrastructure;
using ShopManagement.Infrastructure.EFCore;
using System.Collections.Generic;
using System.Linq;

namespace DiscountManagement.Infrastructure.EFCore.Repository
{
    public class ColleagueDiscountRepository : BaseRepository<long, ColleagueDiscount>, IColleagueDiscountRepository
    {
        private readonly DiscountDbContext _discountContext;
        private readonly ShopDbContext _shopContext;

        public ColleagueDiscountRepository(DiscountDbContext context, ShopDbContext shopContext) : base(context)
        {
            _discountContext = context;
            _shopContext = shopContext;
        }

        public EditColleagueDiscount GetDetails(long id) => _discountContext.ColleagueDiscounts.Select(cd => new EditColleagueDiscount
        {
            Id = id,
            DiscountRate = cd.DiscountRate,
            ProductId = cd.ProductId,
        }).FirstOrDefault(cd => cd.Id == id);

        public List<ColleagueDiscountViewModel> Search(ColleagueDiscountSearchModel searchModel)
        {
            var products = _shopContext.Products.Select(p => new { p.Id, p.Name }).ToList();
            var query = _discountContext.ColleagueDiscounts.Select(cd => new ColleagueDiscountViewModel
            {
                Id = cd.Id,
                CreationDate = cd.CreationDate.ToFarsi(),
                DiscountRate = cd.DiscountRate,
                ProductId = cd.ProductId,
                IsRemoved = cd.IsRemoved
            });
            if (searchModel.ProductId > 0)
                query = query.Where(cd => cd.ProductId == searchModel.ProductId);
            var discounts = query.OrderByDescending(cd => cd.Id).ToList();
            discounts.ForEach(d => d.Product = products.FirstOrDefault(x => x.Id == d.ProductId)?.Name);
            return discounts;
        }
    }
}