using _01.Framework.Application;
using ShopManagement.Application.Contracts.ProductCategoryAggregate;
using ShopManagement.Domain.ProductCategoryAggregate;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace ShopManagement.Application.Implementations
{
    public class ProductCategoryApplication : IProductCategoryApplication
    {
        private readonly IProductCategoryRepository _repository;

        public ProductCategoryApplication(IProductCategoryRepository repository)
        {
            _repository = repository;
        }

        public OperationResult Create(CreateProductCategory command)
        {
            var operation = new OperationResult();
            if (_repository.DoesExist(x => x.Name == command.Name))
                return operation.Failed("امکان ثبت رکورد تکراری وجود ندارد. لطفا مجددا تلاش کنید");
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
                return operation.Failed("رکورد با اطلاعات داده شده یافت نشد. لطفا مجددا تلاش کنید");
            if (_repository.DoesExist(x => x.Name == command.Name && x.Id != command.Id))
                return operation.Failed("امکان ثبت رکورد تکراری وجود ندارد. لطفا مجددا تلاش کنید");
            var slug = command.Slug.Slugify();
            productCategory.Edit(command.Name, command.Description, command.Picture,
                command.PictureAlt, command.PictureTitle, command.Keywords,
                command.MetaDescription, slug);
            _repository.Save();
            return operation.Succeeded();

        }

        public EditProductCategory GetDetails(long id)
        {
            var productCategory = _repository.GetDetails(id);
            return new EditProductCategory
            {
                Id = productCategory.Id,
                Name = productCategory.Name,
                Description = productCategory.Description,
                PictureAlt = productCategory.PictureAlt,
                PictureTitle = productCategory.PictureTitle,
                Keywords = productCategory.Keywords,
                MetaDescription = productCategory.MetaDescription,
                Picture = productCategory.Picture,
                Slug = productCategory.Slug
            };
        }

        public List<ProductCategoryViewModel> Search(ProductCategorySearchModel searchModel)
        {
            return _repository.Search(searchModel.Name).Select(x => new ProductCategoryViewModel
            {
                Id = x.Id,
                Name = x.Name,
                Picture = x.Picture,
                CreationDate = x.CreationDate.ToString(CultureInfo.InvariantCulture),
                //ProductsCount = x.
            }).ToList();
        }
    }
}
