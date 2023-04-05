using ShopManagement.Application.Contracts.ProductPictureAggregate;
using ShopManagement.Application.Contracts.Shared;
using ShopManagement.Domain.ProductPictureAggregate;
using ShopManagement.Domain.Shared;
using System.Collections.Generic;

namespace ShopManagement.Application.Implementations
{
    public class ProductPictureApplication : IProductPictureApplication
    {
        private readonly IProductPictureRepository _repository;

        public ProductPictureApplication(IProductPictureRepository repository)
        {
            _repository = repository;
        }
        public OperationResult Create(CreateProductPicture command)
        {
            var operation = new OperationResult();

            if (_repository.DoesExist(pp => pp.Picture == command.Picture && pp.ProductId == command.ProductId))
            {
                return operation.Failed(ApplicationMessages.DuplicatedRecord);
            }

            var productPricture = new ProductPicture
            (
                command.ProductId,
                command.Picture,
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
            var productPicture = _repository.Get(command.Id);

            if (productPicture is null)
            {
                return operation.Failed(ApplicationMessages.RecordNotFound);
            }

            if (_repository.DoesExist(pp => pp.Picture == command.Picture && pp.ProductId == pp.ProductId && pp.Id != command.Id))
            {
                return operation.Failed(ApplicationMessages.DuplicatedRecord);
            }

            productPicture.Edit
            (
                command.ProductId,
                command.Picture,
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
            {
                return operation.Failed(ApplicationMessages.RecordNotFound);
            }

            productPicture.Remove();
            _repository.Save();
            return operation.Succeeded();
        }
        public OperationResult Restore(long id)
        {
            var operation = new OperationResult();
            var productPicture = _repository.Get(id);

            if (productPicture is null)
            {
                return operation.Failed(ApplicationMessages.RecordNotFound);
            }

            productPicture.Restore();
            _repository.Save();
            return operation.Succeeded();
        }

        public List<ProductPictureViewModel> Search(ProductPictureSearchModel searchModel) => _repository.Search(searchModel);
    }
}
