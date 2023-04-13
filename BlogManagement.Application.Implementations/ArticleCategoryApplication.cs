using BlogManagement.Application.Contracts.ArticleCategoryAggregate;
using BlogManagement.Domain.ArticleCategoryAggregate;
using Framework.Application;
using System.Collections.Generic;

namespace BlogManagement.Application.Implementations
{
    public class ArticleCategoryApplication : IArticleCategoryApplication
    {
        private readonly IArticleCategoryRepository _categoryRepository;
        private readonly IFileUploader _fileUploader;

        public ArticleCategoryApplication(IArticleCategoryRepository categoryRepository, IFileUploader fileUploader)
        {
            _categoryRepository = categoryRepository;
            _fileUploader = fileUploader;
        }

        public OperationResult Create(CreateArticleCategory command)
        {
            OperationResult operation = new OperationResult();
            if (_categoryRepository.DoesExist(ac => ac.Name == command.Name))
                return operation.Failed(ApplicationMessages.DuplicatedRecord);

            string slug = command.Slug.Slugify();
            string pictureName = _fileUploader.Upload(command.Picture, slug);
            ArticleCategory articleCategory = new ArticleCategory
            (
                command.Name, pictureName, command.PictureAlt, command.PictureTitle,
                command.Description, command.ShowOrder, slug, command.Keywords,
                command.MetaDescription, command.CanonicalAddress
            );

            _categoryRepository.Create(articleCategory);
            _categoryRepository.Save();
            return operation.Succeeded();
        }

        public OperationResult Edit(EditArticleCategory command)
        {
            OperationResult operation = new OperationResult();
            ArticleCategory articleCategory = _categoryRepository.Get(command.Id);
            if (articleCategory is null)
                return operation.Failed(ApplicationMessages.RecordNotFound);

            if (_categoryRepository.DoesExist(ac => ac.Name == command.Name && ac.Id != command.Id))
                return operation.Failed(ApplicationMessages.DuplicatedRecord);

            string slug = command.Slug.Slugify();
            string pictureName = _fileUploader.Upload(command.Picture, slug);
            articleCategory.Edit
            (
                command.Name, pictureName, command.PictureAlt, command.PictureTitle,
                command.Description, command.ShowOrder, slug, command.Keywords,
                command.MetaDescription, command.CanonicalAddress
            );

            _categoryRepository.Save();
            return operation.Succeeded();
        }

        public List<ArticleCategoryViewModel> GetArticleCategories() => _categoryRepository.GetArticleCategories();
        public EditArticleCategory GetDetails(long id) => _categoryRepository.GetDetails(id);
        public ArticleCategoryViewModel GetFullDescription(long id)
        {
            var category = _categoryRepository.Get(id);
            return new ArticleCategoryViewModel
            {
                Name = category.Name,
                Description = category.Description
            };
        }

        public List<ArticleCategoryViewModel> Search(ArticleCategorySearchModel searchModel) => _categoryRepository.Search(searchModel);
    }
}
