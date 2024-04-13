using BlogManagement.Application.Contracts.ArticleAggregate;
using BlogManagement.Domain.ArticleAggregate;
using BlogManagement.Domain.ArticleCategoryAggregate;
using Common.Application;
using System;
using System.Collections.Generic;

namespace BlogManagement.Application.Implementations;

public class ArticleApplication(IArticleRepository repository, IFileUploader fileUploader, IArticleCategoryRepository categoryRepository) : IArticleApplication
{
    public OperationResult Create(CreateArticle command)
    {
        OperationResult operation = new OperationResult();
        if (repository.Exists(a => a.Title == command.Title))
            return operation.Failed(ApplicationMessages.DuplicatedRecord);

        string slug = command.Slug.Slugify();
        string categorySlug = categoryRepository.GetSlug(command.CategoryId);
        string path = $"{categorySlug}/{slug}";
        string pictureName = fileUploader.Upload(command.Picture, path);
        DateTime publishDate = command.PublishDate.ToGeorgianDateTime();
        Article article = new Article
        (
            command.Title, command.ShortDescription, command.Description,
            pictureName, command.PictureAlt, command.PictureTitle,
            publishDate, slug, command.MetaDescription, command.Keywords,
            command.CanonicalAddress, command.CategoryId
        );

        repository.Create(article);
        repository.Save();
        return operation.Succeeded();
    }

    public OperationResult Edit(EditArticle command)
    {
        OperationResult operation = new OperationResult();
        Article article = repository.GetWithCategory(command.Id);
        if (article is null)
            return operation.Failed(ApplicationMessages.RecordNotFound);

        if (repository.Exists(a => a.Title == command.Title && a.Id != command.Id))
            return operation.Failed(ApplicationMessages.DuplicatedRecord);

        string slug = command.Slug.Slugify();
        string path = $"{article.Category.Slug}/{slug}";
        string pictureName = fileUploader.Upload(command.Picture, path);
        DateTime publishDate = command.PublishDate.ToGeorgianDateTime();
        article.Edit
        (
            command.Title, command.ShortDescription, command.Description,
            pictureName, command.PictureAlt, command.PictureTitle,
            publishDate, slug, command.MetaDescription, command.Keywords,
            command.CanonicalAddress, command.CategoryId
        );

        repository.Save();
        return operation.Succeeded();
    }

    public EditArticle GetDetails(long id) => repository.GetDetails(id);
    public ArticleViewModel GetFullShortDescription(long id)
    {
        var article = repository.Get(id);
        ArticleViewModel model = new ArticleViewModel
        {
            Title = article.Title,
            ShortDescription = article.ShortDescription,
        };

        return model;
    }

    public List<ArticleViewModel> Search(ArticleSearchModel searchModel) => repository.Search(searchModel);
}
