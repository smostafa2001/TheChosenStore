using BlogManagement.Application.Contracts.ArticleAggregate;
using Framework.Domain;
using System.Collections.Generic;

namespace BlogManagement.Domain.ArticleAggregate
{
    public interface IArticleRepository : IRepository<long, Article>
    {
        EditArticle GetDetails(long id);
        Article GetWithCategory(long id);
        List<ArticleViewModel> Search(ArticleSearchModel searchModel);
    }
}
