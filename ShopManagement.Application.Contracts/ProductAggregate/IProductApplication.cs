﻿using ShopManagement.Application.Contracts.Shared;
using ShopManagement.Domain.ProductAggregate;
using System.Collections.Generic;

namespace ShopManagement.Application.Contracts.ProductAggregate
{
    public interface IProductApplication
    {
        OperationResult Create(CreateProduct command);

        OperationResult Edit(EditProduct command);

        EditProduct GetDetails(long id);

        List<ProductViewModel> GetProducts();

        List<ProductViewModel> Search(ProductSearchModel searchModel);
    }
}