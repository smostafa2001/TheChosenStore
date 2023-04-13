using BlogManagement.Application.Contracts.ArticleAggregate;
using BlogManagement.Domain.ArticleAggregate;
using BlogManagement.Domain.ArticleCategoryAggregate;
using Framework.Application;
using System;
using System.Collections.Generic;

namespace BlogManagement.Application.Implementations
{
    public class ArticleApplication : IArticleApplication
    {
        private readonly IFileUploader _fileUploader;
        private readonly IArticleRepository _repository;
        private readonly IArticleCategoryRepository _categoryRepository;

        public ArticleApplication(IArticleRepository repository, IFileUploader fileUploader, IArticleCategoryRepository categoryRepository)
        {
            _repository = repository;
            _fileUploader = fileUploader;
            _categoryRepository = categoryRepository;
        }

        public OperationResult Create(CreateArticle command)
        {
            OperationResult operation = new OperationResult();
            if (_repository.DoesExist(a => a.Title == command.Title))
                return operation.Failed(ApplicationMessages.DuplicatedRecord);

            string slug = command.Slug.Slugify();
            string categorySlug = _categoryRepository.GetSlug(command.CategoryId);
            string path = $"{categorySlug}/{slug}";
            string pictureName = _fileUploader.Upload(command.Picture, path);
            DateTime publishDate = command.PublishDate.ToGeorgianDateTime();
            Article article = new Article
            (
                command.Title, command.ShortDescription, command.Description,
                pictureName, command.PictureAlt, command.PictureTitle,
                publishDate, slug, command.MetaDescription, command.Keywords,
                command.CanonicalAddress, command.CategoryId
            );

            _repository.Create(article);
            _repository.Save();
            return operation.Succeeded();
        }

        public OperationResult Edit(EditArticle command)
        {
            OperationResult operation = new OperationResult();
            Article article = _repository.Get(command.Id);
            if (article is null)
                return operation.Failed(ApplicationMessages.RecordNotFound);

            if (_repository.DoesExist(a => a.Title == command.Title && a.Id != command.Id))
                return operation.Failed(ApplicationMessages.DuplicatedRecord);

            string slug = command.Slug.Slugify();
            string path = $"{article.Category.Slug}/{slug}";
            string pictureName = _fileUploader.Upload(command.Picture, path);
            DateTime publishDate = command.PublishDate.ToGeorgianDateTime();
            article.Edit
            (
                command.Title, command.ShortDescription, command.Description,
                pictureName, command.PictureAlt, command.PictureTitle,
                publishDate, slug, command.MetaDescription, command.Keywords,
                command.CanonicalAddress, command.CategoryId
            );

            _repository.Save();
            return operation.Succeeded();
        }

        public EditArticle GetDetails(long id) => _repository.GetDetails(id);
        public ArticleViewModel GetFullShortDescription(long id)
        {
            var article = _repository.Get(id);
            ArticleViewModel model = new ArticleViewModel
            {
                Title = article.Title,
                ShortDescription = article.ShortDescription,
            };

            return model;
        }

        public List<ArticleViewModel> Search(ArticleSearchModel searchModel) => _repository.Search(searchModel);
    }
}
