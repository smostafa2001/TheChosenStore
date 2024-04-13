using System.Collections.Generic;

namespace DecorativeStoreQuery.Contracts.ArticleAggregate;

public interface IArticleQuery
{
    List<ArticleQueryModel> GetLatestArticles();
    ArticleQueryModel GetArticleDetails(string slug);
}
