using System.Collections.Generic;

namespace TheChosenStoreQuery.Contracts.ArticleAggregate;

public interface IArticleQuery
{
    List<ArticleQueryModel> GetLatestArticles();
    ArticleQueryModel GetArticleDetails(string slug);
}
