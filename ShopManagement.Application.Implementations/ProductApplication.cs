using Framework.Application;
using ShopManagement.Application.Contracts.ProductAggregate;
using ShopManagement.Domain.ProductAggregate;
using ShopManagement.Domain.ProductCategoryAggregate;
using System.Collections.Generic;

namespace ShopManagement.Application.Implementations
{
    public class ProductApplication : IProductApplication
    {
        private readonly IFileUploader _fileUploader;
        private readonly IProductRepository _repository;
        private readonly IProductCategoryRepository _categoryRepository;
        public ProductApplication(IProductRepository repository, IFileUploader fileUploader, IProductCategoryRepository categoryRepository)
        {
            _repository = repository;
            _fileUploader = fileUploader;
            _categoryRepository = categoryRepository;
        }

        public OperationResult Create(CreateProduct command)
        {
            var operation = new OperationResult();
            if (_repository.DoesExist(x => x.Name == command.Name))
                return operation.Failed(ApplicationMessages.DuplicatedRecord);

            var slug = command.Slug.Slugify();
            var categorySlug = _categoryRepository.GetSlug(command.CategoryId);
            var path = $"{categorySlug}/{slug}";
            var picturePath = _fileUploader.Upload(command.Picture, path);
            var product = new Product(command.Name, command.Code, command.ShortDescription,
                command.Description, picturePath, command.PictureAlt,
                command.PictureTitle, command.CategoryId,
                slug, command.Keywords, command.MetaDescription);
            _repository.Create(product);
            _repository.Save();
            return operation.Succeeded();
        }

        public OperationResult Edit(EditProduct command)
        {
            var operation = new OperationResult();
            var product = _repository.GetProductWithCategory(command.Id);
            if (product == null)
                return operation.Failed(ApplicationMessages.RecordNotFound);
            if (_repository.DoesExist(x => x.Name == command.Name && x.Id != command.Id))
                return operation.Failed(ApplicationMessages.DuplicatedRecord);
            var slug = command.Slug.Slugify();
            var path = $"{product.Category.Slug}/{slug}";
            var picturePath = _fileUploader.Upload(command.Picture, path);
            product.Edit(command.Name, command.Code, command.ShortDescription,
                command.Description, picturePath, command.PictureAlt,
                command.PictureTitle, command.CategoryId,
                slug, command.Keywords, command.MetaDescription);
            _repository.Save();
            return operation.Succeeded();
        }

        public EditProduct GetDetails(long id) => _repository.GetDetails(id);

        public List<ProductViewModel> GetProducts() => _repository.GetProducts();

        public List<ProductViewModel> Search(ProductSearchModel searchModel) => _repository.Search(searchModel);
    }
}