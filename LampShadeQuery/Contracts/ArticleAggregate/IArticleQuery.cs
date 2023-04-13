using System.Collections.Generic;

namespace LampShadeQuery.Contracts.ArticleAggregate
{
    public interface IArticleQuery
    {
        List<ArticleQueryModel> GetLatestArticles();
        ArticleQueryModel GetArticleDetails(string slug);
    }
}
