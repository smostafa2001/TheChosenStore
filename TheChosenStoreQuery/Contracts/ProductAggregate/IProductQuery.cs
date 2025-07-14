﻿using ShopManagement.Application.Contracts.OrderAggregate;
using System.Collections.Generic;

namespace TheChosenStoreQuery.Contracts.ProductAggregate;

public interface IProductQuery
{
    ProductQueryModel GetDetails(string slug);
    List<ProductQueryModel> GetLatestArrivals();
    List<ProductQueryModel> Search(string value);
    List<CartItem> CheckInventoryStatus(List<CartItem> cartItems);
}
