﻿using Common.Application;
using System.Collections.Generic;

namespace ShopManagement.Application.Contracts.ProductCategoryAggregate;

public interface IProductCategoryApplication
{
    OperationResult Create(CreateProductCategory command);

    OperationResult Edit(EditProductCategory command);

    List<ProductCategoryViewModel> GetProductCategories();

    EditProductCategory GetDetails(long id);

    List<ProductCategoryViewModel> Search(ProductCategorySearchModel searchModel);
}