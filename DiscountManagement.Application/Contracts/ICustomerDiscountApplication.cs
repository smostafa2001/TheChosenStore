using DiscountManagement.Application.Shared;
using DiscountManagement.Domain.CustomerDiscountAggregate;
using System.Collections.Generic;

namespace DiscountManagement.Application.Contracts
{
    public interface ICustomerDiscountApplication
    {
        OperationResult Define(DefineCustomerDiscount command);

        OperationResult Edit(EditCustomerDiscount command);

        EditCustomerDiscount GetDetails(long id);

        List<CustomerDiscountViewModel> Search(CustomerDiscountSearchModel searchModel);
    }
}