using Common.Application;
using DiscountManagement.Application.Contracts.CustomerDiscountAggregate;
using DiscountManagement.Domain.CustomerDiscountAggregate;
using ShopManagement.Domain.ProductAggregate;
using System.Collections.Generic;

namespace DiscountManagement.Application.Implementations;

public class CustomerDiscountApplication(ICustomerDiscountRepository repository, IProductRepository productRepository) : ICustomerDiscountApplication
{
    public OperationResult Define(DefineCustomerDiscount command)
    {
        var operation = new OperationResult();
        if (repository.Exists(cd => cd.ProductId == command.ProductId && cd.DiscountRate == command.DiscountRate))
            return operation.Failed(ApplicationMessages.DuplicatedRecord);
        var startDate = command.StartDate.ToGeorgianDateTime();
        var endDate = command.EndDate.ToGeorgianDateTime();
        var customerDiscount = new CustomerDiscount
        (
            command.ProductId,
            command.DiscountRate,
            startDate,
            endDate,
            command.Reason
        );
        repository.Create(customerDiscount);
        repository.Save();
        return operation.Succeeded();
    }

    public OperationResult Edit(EditCustomerDiscount command)
    {
        var operation = new OperationResult();
        var customerDiscount = repository.Get(command.Id);

        if (customerDiscount is null)
            return operation.Failed(ApplicationMessages.RecordNotFound);

        if (repository.Exists(cd => cd.ProductId == command.ProductId && cd.DiscountRate == command.DiscountRate && cd.Id != command.Id))
            return operation.Failed(ApplicationMessages.DuplicatedRecord);

        var startDate = command.StartDate.ToGeorgianDateTime();
        var endDate = command.EndDate.ToGeorgianDateTime();
        customerDiscount.Edit
        (
            command.ProductId,
            command.DiscountRate,
            startDate,
            endDate,
            command.Reason
        );
        repository.Save();
        return operation.Succeeded();
    }

    public EditCustomerDiscount GetDetails(long id) => repository.GetDetails(id);
    public CustomerDiscountViewModel GetFullReason(long id)
    {
        var customerDiscount = repository.Get(id);
        return new CustomerDiscountViewModel
        {
            Product = productRepository.Get(customerDiscount.ProductId).Name,
            Reason = customerDiscount.Reason
        };
    }
    public List<CustomerDiscountViewModel> Search(CustomerDiscountSearchModel searchModel) => repository.Search(searchModel);
}
