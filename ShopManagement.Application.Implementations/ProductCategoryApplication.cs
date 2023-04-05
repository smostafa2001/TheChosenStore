using ShopManagement.Application.Contracts.ProductCategoryAggregate;
using ShopManagement.Application.Contracts.Shared;
using ShopManagement.Domain.ProductCategoryAggregate;
using ShopManagement.Domain.Shared;
using System.Collections.Generic;

namespace ShopManagement.Application.Implementations
{
    public class ProductCategoryApplication : IProductCategoryApplication
    {
        private readonly IProductCategoryRepository _repository;

        public ProductCategoryApplication(IProductCategoryRepository repository) => _repository = repository;

        public OperationResult Create(CreateProductCategory command)
        {
            var operation = new OperationResult();
            if (_repository.DoesExist(x => x.Name == command.Name))
                return operation.Failed(ApplicationMessages.DuplicatedRecord);
            var slug = command.Slug.Slugify();
            var productCategory = new ProductCategory(command.Name, command.Description, command.Picture,
                command.PictureAlt, command.PictureTitle, command.Keywords,
                command.MetaDescription, slug);
            _repository.Create(productCategory);
            _repository.Save();
            return operation.Succeeded();

        }
        public OperationResult Edit(EditProductCategory command)
        {
            var operation = new OperationResult();
            var productCategory = _repository.Get(command.Id);
            if (productCategory == null)
                return operation.Failed(ApplicationMessages.RecordNotFound);
            if (_repository.DoesExist(x => x.Name == command.Name && x.Id != command.Id))
                return operation.Failed(ApplicationMessages.DuplicatedRecord);
            var slug = command.Slug.Slugify();
            productCategory.Edit(command.Name, command.Description, command.Picture,
                command.PictureAlt, command.PictureTitle, command.Keywords,
                command.MetaDescription, slug);
            _repository.Save();
            return operation.Succeeded();

        }
        public EditProductCategory GetDetails(long id) => _repository.GetDetails(id);
        public List<ProductCategoryViewModel> GetProductCategories() => _repository.GetProductCategories();
        public List<ProductCategoryViewModel> Search(ProductCategorySearchModel searchModel) => _repository.Search(searchModel);
    }
}
