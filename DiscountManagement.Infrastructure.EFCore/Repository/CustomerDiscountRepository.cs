using Common.Application;
using Common.Infrastructure;
using DiscountManagement.Application.Contracts.CustomerDiscountAggregate;
using DiscountManagement.Domain.CustomerDiscountAggregate;
using ShopManagement.Infrastructure.EFCore;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace DiscountManagement.Infrastructure.EFCore.Repository;

public class CustomerDiscountRepository(DiscountDbContext context, ShopDbContext shopContext) : BaseRepository<long, CustomerDiscount>(context), ICustomerDiscountRepository
{
    public EditCustomerDiscount GetDetails(long id) => context.CustomerDiscounts.Select(cd => new EditCustomerDiscount
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
        var products = shopContext.Products.Select(p => new { p.Id, p.Name }).ToList();
        var query = context.CustomerDiscounts.Select(cd => new CustomerDiscountViewModel
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