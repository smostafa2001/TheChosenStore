using BlogManagement.Domain.ArticleCategoryAggregate;
using Common.Domain;
using System;

namespace BlogManagement.Domain.ArticleAggregate;

public class Article(
    string title, string shortDescription, string description,
    string picture, string pictureAlt, string pictureTitle,
    DateTime publishDate, string slug, string metaDescription,
    string keywords, string canonicalAddress, long categoryId
    ) : EntityBase
{
    public string Title { get; private set; } = title;
    public string ShortDescription { get; private set; } = shortDescription;
    public string Description { get; private set; } = description;
    public string Picture { get; private set; } = picture;
    public string PictureAlt { get; private set; } = pictureAlt;
    public string PictureTitle { get; private set; } = pictureTitle;
    public DateTime PublishDate { get; private set; } = publishDate;
    public string Slug { get; private set; } = slug;
    public string MetaDescription { get; private set; } = metaDescription;
    public string Keywords { get; private set; } = keywords;
    public string CanonicalAddress { get; private set; } = canonicalAddress;
    public long CategoryId { get; private set; } = categoryId;
    public ArticleCategory Category { get; private set; }

    public void Edit
    (
        string title, string shortDescription, string description,
        string picture, string pictureAlt, string pictureTitle,
        DateTime publishDate, string slug, string metaDescription,
        string keywords, string canonicalAddress, long categoryId
    )
    {
        Title = title;
        ShortDescription = shortDescription;
        Description = description;
        if (!string.IsNullOrWhiteSpace(picture))
            Picture = picture;
        PictureAlt = pictureAlt;
        PictureTitle = pictureTitle;
        PublishDate = publishDate;
        Slug = slug;
        MetaDescription = metaDescription;
        Keywords = keywords;
        CanonicalAddress = canonicalAddress;
        CategoryId = categoryId;
    }
}
