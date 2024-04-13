using Common.Domain;
using System;

namespace DiscountManagement.Domain.CustomerDiscountAggregate;

public class CustomerDiscount(long productId, int discountRate, DateTime startDate, DateTime endDate, string reason) : EntityBase
{
    public int DiscountRate { get; private set; } = discountRate;
    public DateTime StartDate { get; private set; } = startDate;
    public DateTime EndDate { get; private set; } = endDate;
    public string Reason { get; private set; } = reason;
    public long ProductId { get; private set; } = productId;

    public void Edit(long productId, int discountRate, DateTime startDate, DateTime endDate, string reason)
    {
        ProductId = productId;
        DiscountRate = discountRate;
        StartDate = startDate;
        EndDate = endDate;
        Reason = reason;
    }
}