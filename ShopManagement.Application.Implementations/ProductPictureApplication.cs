using Common.Application;
using ShopManagement.Application.Contracts.ProductPictureAggregate;
using ShopManagement.Domain.ProductAggregate;
using ShopManagement.Domain.ProductPictureAggregate;
using System.Collections.Generic;

namespace ShopManagement.Application.Implementations;

public class ProductPictureApplication(IProductPictureRepository repository, IProductRepository productRepository, IFileUploader fileUploader) : IProductPictureApplication
{
    public OperationResult Create(CreateProductPicture command)
    {
        var operation = new OperationResult();

        var product = productRepository.GetProductWithCategory(command.ProductId);
        var path = $"{product.Category.Slug}/{product.Slug}";
        var picturePath = fileUploader.Upload(command.Picture, path);

        var productPricture = new ProductPicture
        (
            command.ProductId,
            picturePath,
            command.PictureAlt,
            command.PictureTitle
        );
        repository.Create(productPricture);
        repository.Save();

        return operation.Succeeded();
    }

    public OperationResult Edit(EditProductPicture command)
    {
        var operation = new OperationResult();
        var productPicture = repository.GetProductPictureWithProductAndCategory(command.Id);
        var path = $"{productPicture.Product.Category.Slug}/{productPicture.Product.Slug}";
        var picturePath = fileUploader.Upload(command.Picture, path);

        if (productPicture is null)
            return operation.Failed(ApplicationMessages.RecordNotFound);

        productPicture.Edit
        (
            command.ProductId,
            picturePath,
            command.PictureAlt,
            command.PictureTitle
        );
        repository.Save();

        return operation.Succeeded();
    }

    public EditProductPicture GetDetails(long id) => repository.GetDetails(id);

    public OperationResult Remove(long id)
    {
        var operation = new OperationResult();
        var productPicture = repository.Get(id);

        if (productPicture is null)
            return operation.Failed(ApplicationMessages.RecordNotFound);

        productPicture.Remove();
        repository.Save();
        return operation.Succeeded();
    }

    public OperationResult Restore(long id)
    {
        var operation = new OperationResult();
        var productPicture = repository.Get(id);

        if (productPicture is null)
            return operation.Failed(ApplicationMessages.RecordNotFound);

        productPicture.Restore();
        repository.Save();
        return operation.Succeeded();
    }

    public List<ProductPictureViewModel> Search(ProductPictureSearchModel searchModel) => repository.Search(searchModel);
}