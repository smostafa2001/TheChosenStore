using System.Collections.Generic;
using TheChosenStoreQuery.Contracts.ArticleAggregate;

namespace TheChosenStoreQuery.Contracts.ArticleCategoryAggregate;

public class ArticleCategoryQueryModel
{
    public string Name { get; set; }
    public string Picture { get; set; }
    public string PictureAlt { get; set; }
    public string PictureTitle { get; set; }
    public string Description { get; set; }
    public int ShowOrder { get; set; }
    public string Slug { get; set; }
    public string Keywords { get; set; }
    public List<string> KeywordsList { get; set; }
    public string MetaDescription { get; set; }
    public string CanonicalAddress { get; set; }
    public List<ArticleQueryModel> Articles { get; set; }
    public long ArticlesCount { get; set; }
}
