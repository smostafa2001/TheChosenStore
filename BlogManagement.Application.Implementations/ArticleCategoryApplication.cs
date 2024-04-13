using BlogManagement.Application.Contracts.ArticleCategoryAggregate;
using BlogManagement.Domain.ArticleCategoryAggregate;
using Common.Application;
using System.Collections.Generic;

namespace BlogManagement.Application.Implementations;

public class ArticleCategoryApplication(IArticleCategoryRepository categoryRepository, IFileUploader fileUploader) : IArticleCategoryApplication
{
    public OperationResult Create(CreateArticleCategory command)
    {
        OperationResult operation = new OperationResult();
        if (categoryRepository.Exists(ac => ac.Name == command.Name))
            return operation.Failed(ApplicationMessages.DuplicatedRecord);

        string slug = command.Slug.Slugify();
        string pictureName = fileUploader.Upload(command.Picture, slug);
        ArticleCategory articleCategory = new ArticleCategory
        (
            command.Name, pictureName, command.PictureAlt, command.PictureTitle,
            command.Description, command.ShowOrder, slug, command.Keywords,
            command.MetaDescription, command.CanonicalAddress
        );

        categoryRepository.Create(articleCategory);
        categoryRepository.Save();
        return operation.Succeeded();
    }

    public OperationResult Edit(EditArticleCategory command)
    {
        OperationResult operation = new OperationResult();
        ArticleCategory articleCategory = categoryRepository.Get(command.Id);
        if (articleCategory is null)
            return operation.Failed(ApplicationMessages.RecordNotFound);

        if (categoryRepository.Exists(ac => ac.Name == command.Name && ac.Id != command.Id))
            return operation.Failed(ApplicationMessages.DuplicatedRecord);

        string slug = command.Slug.Slugify();
        string pictureName = fileUploader.Upload(command.Picture, slug);
        articleCategory.Edit
        (
            command.Name, pictureName, command.PictureAlt, command.PictureTitle,
            command.Description, command.ShowOrder, slug, command.Keywords,
            command.MetaDescription, command.CanonicalAddress
        );

        categoryRepository.Save();
        return operation.Succeeded();
    }

    public List<ArticleCategoryViewModel> GetArticleCategories() => categoryRepository.GetArticleCategories();
    public EditArticleCategory GetDetails(long id) => categoryRepository.GetDetails(id);
    public ArticleCategoryViewModel GetFullDescription(long id)
    {
        var category = categoryRepository.Get(id);
        return new ArticleCategoryViewModel
        {
            Name = category.Name,
            Description = category.Description
        };
    }

    public List<ArticleCategoryViewModel> Search(ArticleCategorySearchModel searchModel) => categoryRepository.Search(searchModel);
}
