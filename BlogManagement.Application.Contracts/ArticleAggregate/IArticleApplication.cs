using Framework.Application;
using System.Collections.Generic;

namespace BlogManagement.Application.Contracts.ArticleAggregate
{
    public interface IArticleApplication
    {
        OperationResult Create(CreateArticle command);
        OperationResult Edit(EditArticle command);
        EditArticle GetDetails(long id);
        ArticleViewModel GetFullShortDescription(long id);
        List<ArticleViewModel> Search(ArticleSearchModel searchModel);
    }
}
