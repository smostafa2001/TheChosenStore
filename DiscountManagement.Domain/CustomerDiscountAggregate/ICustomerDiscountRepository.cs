using Common.Domain;
using DiscountManagement.Application.Contracts.CustomerDiscountAggregate;
using System.Collections.Generic;

namespace DiscountManagement.Domain.CustomerDiscountAggregate;

public interface ICustomerDiscountRepository : IRepository<long, CustomerDiscount>
{
    EditCustomerDiscount GetDetails(long id);

    List<CustomerDiscountViewModel> Search(CustomerDiscountSearchModel searchModel);
}