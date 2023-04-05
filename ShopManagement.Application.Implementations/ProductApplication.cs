using ShopManagement.Application.Contracts.ProductAggregate;
using ShopManagement.Application.Contracts.Shared;
using ShopManagement.Domain.ProductAggregate;
using ShopManagement.Domain.Shared;
using System.Collections.Generic;

namespace ShopManagement.Application.Implementations
{
    public class ProductApplication : IProductApplication
    {
        private readonly IProductRepository _repository;

        public ProductApplication(IProductRepository repository) => _repository = repository;

        public OperationResult Create(CreateProduct command)
        {
            var operation = new OperationResult();
            if (_repository.DoesExist(x => x.Name == command.Name))
                return operation.Failed(ApplicationMessages.DuplicatedRecord);

            var slug = command.Slug.Slugify();
            var product = new Product(command.Name, command.Code, command.UnitPrice,
                command.ShortDescription, command.Description, command.Picture,
                command.PictureAlt, command.PictureTitle, command.CategoryId,
                slug, command.Keywords, command.MetaDescription);
            _repository.Create(product);
            _repository.Save();
            return operation.Succeeded();
        }

        public OperationResult Edit(EditProduct command)
        {
            var operation = new OperationResult();
            var product = _repository.Get(command.Id);
            if (product == null)
                return operation.Failed(ApplicationMessages.RecordNotFound);
            if (_repository.DoesExist(x => x.Name == command.Name && x.Id != command.Id))
                return operation.Failed(ApplicationMessages.DuplicatedRecord);
            var slug = command.Slug.Slugify();
            product.Edit(command.Name, command.Code, command.UnitPrice,
                command.ShortDescription, command.Description, command.Picture,
                command.PictureAlt, command.PictureTitle, command.CategoryId,
                slug, command.Keywords, command.MetaDescription);
            _repository.Save();
            return operation.Succeeded();
        }

        public EditProduct GetDetails(long id) => _repository.GetDetails(id);
        public List<ProductViewModel> GetProducts() => _repository.GetProducts();
        public OperationResult MakeAvailableInStock(long id)
        {
            var operation = new OperationResult();
            var product = _repository.Get(id);
            if (product == null)
                return operation.Failed(ApplicationMessages.RecordNotFound);

            product.MakeAvailableInStock();
            _repository.Save();
            return operation.Succeeded();
        }

        public OperationResult MakeUnavailableInStock(long id)
        {
            var operation = new OperationResult();
            var product = _repository.Get(id);
            if (product == null)
                return operation.Failed(ApplicationMessages.RecordNotFound);

            product.MakeUnavailableInStock();
            _repository.Save();
            return operation.Succeeded();
        }

        public List<ProductViewModel> Search(ProductSearchModel searchModel) => _repository.Search(searchModel);
    }
}
