using Common.Domain;
using ShopManagement.Domain.ProductAggregate;

namespace ShopManagement.Domain.ProductPictureAggregate;

public class ProductPicture(long productId, string picture, string pictureAlt, string pictureTitle) : EntityBase
{
    public string Picture { get; private set; } = picture;
    public string PictureAlt { get; private set; } = pictureAlt;
    public string PictureTitle { get; private set; } = pictureTitle;
    public bool IsRemoved { get; private set; } = false;
    public long ProductId { get; private set; } = productId;
    public Product Product { get; private set; }

    public void Edit(long productId, string picture, string pictureAlt, string pictureTitle)
    {
        ProductId = productId;
        if (!string.IsNullOrWhiteSpace(picture))
            Picture = picture;
        PictureAlt = pictureAlt;
        PictureTitle = pictureTitle;
    }

    public void Remove() => IsRemoved = true;

    public void Restore() => IsRemoved = false;
}