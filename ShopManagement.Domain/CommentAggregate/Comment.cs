using ShopManagement.Domain.ProductAggregate;
using ShopManagement.Domain.Shared;

namespace ShopManagement.Domain.CommentAggregate
{
    public class Comment : EntityBase
    {
        public string Name { get; private set; }
        public string Email { get; private set; }
        public string Message { get; private set; }
        public bool IsConfirmed { get; private set; }
        public bool IsCanceled { get; private set; }
        public long ProductId { get; private set; }
        public Product Product { get; private set; }

        public Comment(string name, string email, string message, long productId)
        {
            Name = name;
            Email = email;
            Message = message;
            ProductId = productId;
        }

        public void Confirm() => IsConfirmed = true;
        public void Cancel() => IsCanceled = true;
        public void Restore()
        {
            IsCanceled = false;
            IsConfirmed = false;
        }
        public void Remove()
        {
            IsCanceled = true;
            IsConfirmed = false;
        }
        public void Review()
        {
            IsCanceled = false;
            IsConfirmed = true;
        }


    }
}
