using Framework.Application;
using ShopManagement.Application.Contracts.ProductPictureAggregate;
using ShopManagement.Domain.ProductAggregate;
using ShopManagement.Domain.ProductPictureAggregate;
using System.Collections.Generic;

namespace ShopManagement.Application.Implementations
{
    public class ProductPictureApplication : IProductPictureApplication
    {
        private readonly IFileUploader _fileUploader;
        private readonly IProductPictureRepository _repository;
        private readonly IProductRepository _productRepository;
        public ProductPictureApplication(IProductPictureRepository repository, IProductRepository productRepository, IFileUploader fileUploader)
        {
            _repository = repository;
            _productRepository = productRepository;
            _fileUploader = fileUploader;
        }

        public OperationResult Create(CreateProductPicture command)
        {
            var operation = new OperationResult();

            var product = _productRepository.GetProductWithCategory(command.ProductId);
            var path = $"{product.Category.Slug}/{product.Slug}";
            var picturePath = _fileUploader.Upload(command.Picture, path);

            var productPricture = new ProductPicture
            (
                command.ProductId,
                picturePath,
                command.PictureAlt,
                command.PictureTitle
            );
            _repository.Create(productPricture);
            _repository.Save();

            return operation.Succeeded();
        }

        public OperationResult Edit(EditProductPicture command)
        {
            var operation = new OperationResult();
            var productPicture = _repository.GetProductPictureWithProductAndCategory(command.Id);
            var path = $"{productPicture.Product.Category.Slug}/{productPicture.Product.Slug}";
            var picturePath = _fileUploader.Upload(command.Picture, path);

            if (productPicture is null)
                return operation.Failed(ApplicationMessages.RecordNotFound);

            productPicture.Edit
            (
                command.ProductId,
                picturePath,
                command.PictureAlt,
                command.PictureTitle
            );
            _repository.Save();

            return operation.Succeeded();
        }

        public EditProductPicture GetDetails(long id) => _repository.GetDetails(id);

        public OperationResult Remove(long id)
        {
            var operation = new OperationResult();
            var productPicture = _repository.Get(id);

            if (productPicture is null)
                return operation.Failed(ApplicationMessages.RecordNotFound);

            productPicture.Remove();
            _repository.Save();
            return operation.Succeeded();
        }

        public OperationResult Restore(long id)
        {
            var operation = new OperationResult();
            var productPicture = _repository.Get(id);

            if (productPicture is null)
                return operation.Failed(ApplicationMessages.RecordNotFound);


            productPicture.Restore();
            _repository.Save();
            return operation.Succeeded();
        }

        public List<ProductPictureViewModel> Search(ProductPictureSearchModel searchModel) => _repository.Search(searchModel);
    }
}