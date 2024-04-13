using Common.Domain;

namespace DiscountManagement.Domain.ColleagueDiscountAggregate;

public class ColleagueDiscount(long productId, int discountRate) : EntityBase
{
    public long ProductId { get; private set; } = productId;
    public int DiscountRate { get; private set; } = discountRate;
    public bool IsRemoved { get; set; } = false;

    public void Edit(long productId, int discountRate)
    {
        ProductId = productId;
        DiscountRate = discountRate;
    }

    public void Remove() => IsRemoved = true;

    public void Restore() => IsRemoved = false;
}