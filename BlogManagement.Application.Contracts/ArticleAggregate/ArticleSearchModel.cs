using BlogManagement.Application.Contracts.ArticleCategoryAggregate;
using System.Collections.Generic;

namespace BlogManagement.Application.Contracts.ArticleAggregate
{
    public class ArticleSearchModel
    {
        public string Title { get; set; }
        public long CategoryId { get; set; }
    }
}
