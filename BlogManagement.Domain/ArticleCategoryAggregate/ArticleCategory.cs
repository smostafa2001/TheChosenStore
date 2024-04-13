using BlogManagement.Domain.ArticleAggregate;
using Common.Domain;
using System.Collections.Generic;

namespace BlogManagement.Domain.ArticleCategoryAggregate;

public class ArticleCategory(
    string name, string picture, string pictureAlt,
    string pictureTitle, string description, int showOrder,
    string slug, string keywords, string metaDescription,
    string canonicalAddress
    ) : EntityBase
{
    public string Name { get; private set; } = name;
    public string Picture { get; private set; } = picture;
    public string PictureAlt { get; private set; } = pictureAlt;
    public string PictureTitle { get; private set; } = pictureTitle;
    public string Description { get; private set; } = description;
    public int ShowOrder { get; private set; } = showOrder;
    public string Slug { get; private set; } = slug;
    public string Keywords { get; private set; } = keywords;
    public string MetaDescription { get; private set; } = metaDescription;
    public string CanonicalAddress { get; private set; } = canonicalAddress;
    public List<Article> Articles { get; private set; }

    public void Edit
    (
        string name, string picture, string pictureAlt,
        string pictureTitle, string description, int showOrder,
        string slug, string keywords, string metaDescription,
        string canonicalAddress
    )
    {
        Name = name;
        if (!string.IsNullOrWhiteSpace(picture)) Picture = picture;

        Description = description;
        ShowOrder = showOrder;
        Slug = slug;
        Keywords = keywords;
        MetaDescription = metaDescription;
        CanonicalAddress = canonicalAddress;
        PictureAlt = pictureAlt;
        PictureTitle = pictureTitle;
    }
}
