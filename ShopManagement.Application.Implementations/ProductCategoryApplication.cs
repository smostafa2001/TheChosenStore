using Common.Application;
using ShopManagement.Application.Contracts.ProductCategoryAggregate;
using ShopManagement.Domain.ProductCategoryAggregate;
using System.Collections.Generic;

namespace ShopManagement.Application.Implementations;

public class ProductCategoryApplication(IProductCategoryRepository repository, IFileUploader fileUploader) : IProductCategoryApplication
{
    public OperationResult Create(CreateProductCategory command)
    {
        var operation = new OperationResult();
        if (repository.Exists(x => x.Name == command.Name))
            return operation.Failed(ApplicationMessages.DuplicatedRecord);
        var slug = command.Slug.Slugify();
        var picturePath = $"{command.Slug}";
        var fileName = fileUploader.Upload(command.Picture, picturePath);
        var productCategory = new ProductCategory(command.Name, command.Description, fileName,
            command.PictureAlt, command.PictureTitle, command.Keywords,
            command.MetaDescription, slug);
        repository.Create(productCategory);
        repository.Save();
        return operation.Succeeded();
    }

    public OperationResult Edit(EditProductCategory command)
    {
        var operation = new OperationResult();
        var productCategory = repository.Get(command.Id);
        if (productCategory == null)
            return operation.Failed(ApplicationMessages.RecordNotFound);
        if (repository.Exists(x => x.Name == command.Name && x.Id != command.Id))
            return operation.Failed(ApplicationMessages.DuplicatedRecord);
        var slug = command.Slug.Slugify();
        var picturePath = $"{command.Slug}";
        var fileName = fileUploader.Upload(command.Picture, picturePath);
        productCategory.Edit(command.Name, command.Description, fileName,
            command.PictureAlt, command.PictureTitle, command.Keywords,
            command.MetaDescription, slug);
        repository.Save();
        return operation.Succeeded();
    }

    public EditProductCategory GetDetails(long id) => repository.GetDetails(id);

    public List<ProductCategoryViewModel> GetProductCategories() => repository.GetProductCategories();

    public List<ProductCategoryViewModel> Search(ProductCategorySearchModel searchModel) => repository.Search(searchModel);
}