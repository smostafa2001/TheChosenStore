using Common.Application;
using ShopManagement.Application.Contracts.ProductAggregate;
using ShopManagement.Domain.ProductAggregate;
using ShopManagement.Domain.ProductCategoryAggregate;
using System.Collections.Generic;

namespace ShopManagement.Application.Implementations;

public class ProductApplication(IProductRepository repository, IFileUploader fileUploader, IProductCategoryRepository categoryRepository) : IProductApplication
{
    public OperationResult Create(CreateProduct command)
    {
        var operation = new OperationResult();
        if (repository.Exists(x => x.Name == command.Name))
            return operation.Failed(ApplicationMessages.DuplicatedRecord);

        var slug = command.Slug.Slugify();
        var categorySlug = categoryRepository.GetSlug(command.CategoryId);
        var path = $"{categorySlug}/{slug}";
        var picturePath = fileUploader.Upload(command.Picture, path);
        var product = new Product(
            command.Name, command.Code, command.ShortDescription,
            command.Description, picturePath, command.PictureAlt,
            command.PictureTitle, command.CategoryId,
            slug, command.Keywords, command.MetaDescription
            );

        repository.Create(product);
        repository.Save();
        return operation.Succeeded();
    }

    public OperationResult Edit(EditProduct command)
    {
        var operation = new OperationResult();
        var product = repository.GetProductWithCategory(command.Id);
        if (product == null)
            return operation.Failed(ApplicationMessages.RecordNotFound);
        if (repository.Exists(x => x.Name == command.Name && x.Id != command.Id))
            return operation.Failed(ApplicationMessages.DuplicatedRecord);
        var slug = command.Slug.Slugify();
        var path = $"{product.Category.Slug}/{slug}";
        var picturePath = fileUploader.Upload(command.Picture, path);
        product.Edit(command.Name, command.Code, command.ShortDescription,
            command.Description, picturePath, command.PictureAlt,
            command.PictureTitle, command.CategoryId,
            slug, command.Keywords, command.MetaDescription);
        repository.Save();
        return operation.Succeeded();
    }

    public EditProduct GetDetails(long id) => repository.GetDetails(id);

    public List<ProductViewModel> GetProducts() => repository.GetProducts();

    public List<ProductViewModel> Search(ProductSearchModel searchModel) => repository.Search(searchModel);
}