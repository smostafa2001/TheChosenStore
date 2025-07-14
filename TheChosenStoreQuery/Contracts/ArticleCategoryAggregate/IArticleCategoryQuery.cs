﻿using System.Collections.Generic;

namespace TheChosenStoreQuery.Contracts.ArticleCategoryAggregate;

public interface IArticleCategoryQuery
{
    List<ArticleCategoryQueryModel> GetArticleCategories();
    ArticleCategoryQueryModel GetArticleCategory(string slug);
}
