﻿using Common.Application;
using System.Collections.Generic;

namespace BlogManagement.Application.Contracts.ArticleCategoryAggregate;

public interface IArticleCategoryApplication
{
    OperationResult Create(CreateArticleCategory command);
    OperationResult Edit(EditArticleCategory command);
    EditArticleCategory GetDetails(long id);
    List<ArticleCategoryViewModel> GetArticleCategories();
    List<ArticleCategoryViewModel> Search(ArticleCategorySearchModel searchModel);
    ArticleCategoryViewModel GetFullDescription(long id);
}
