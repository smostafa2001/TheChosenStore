using DiscountManagement.Application.Contracts.CustomerDiscountAggregate;
using DiscountManagement.Domain.CustomerDiscountAggregate;
using Framework.Application;
using ShopManagement.Domain.ProductAggregate;
using System.Collections.Generic;

namespace DiscountManagement.Application.Implementations
{
    public class CustomerDiscountApplication : ICustomerDiscountApplication
    {
        private readonly ICustomerDiscountRepository _repository;
        private readonly IProductRepository _productRepository;

        public CustomerDiscountApplication(ICustomerDiscountRepository repository, IProductRepository productRepository)
        {
            _repository = repository;
            _productRepository = productRepository;
        }

        public OperationResult Define(DefineCustomerDiscount command)
        {
            var operation = new OperationResult();
            if (_repository.DoesExist(cd => cd.ProductId == command.ProductId && cd.DiscountRate == command.DiscountRate))
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
            _repository.Create(customerDiscount);
            _repository.Save();
            return operation.Succeeded();
        }

        public OperationResult Edit(EditCustomerDiscount command)
        {
            var operation = new OperationResult();
            var customerDiscount = _repository.Get(command.Id);

            if (customerDiscount is null)
                return operation.Failed(ApplicationMessages.RecordNotFound);

            if (_repository.DoesExist(cd => cd.ProductId == command.ProductId && cd.DiscountRate == command.DiscountRate && cd.Id != command.Id))
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
            _repository.Save();
            return operation.Succeeded();
        }

        public EditCustomerDiscount GetDetails(long id) => _repository.GetDetails(id);
        public CustomerDiscountViewModel GetFullReason(long id)
        {
            var customerDiscount = _repository.Get(id);
            return new CustomerDiscountViewModel
            {
                Product = _productRepository.Get(customerDiscount.ProductId).Name,
                Reason = customerDiscount.Reason
            };
        }
        public List<CustomerDiscountViewModel> Search(CustomerDiscountSearchModel searchModel) => _repository.Search(searchModel);
    }
}
