using Framework.Domain;
using System.Collections.Generic;

namespace CommentManagement.Domain.CommentAggregate
{
    public class Comment : EntityBase
    {
        public string Name { get; private set; }
        public string Email { get; private set; }
        public string Website { get; private set; }
        public string Message { get; private set; }
        public bool IsConfirmed { get; private set; }
        public bool IsCanceled { get; private set; }
        public long OwnerRecordId { get; private set; }
        public byte Type { get; private set; }
        public long? ParentId { get; private set; }
        public Comment Parent { get; set; }
        public List<Comment> Children { get; private set; }

        public Comment(string name, string email, string website, string message, long ownerRecordId, byte type, long? parentId)
        {
            Name = name;
            Email = email;
            Website = website;
            Message = message;
            OwnerRecordId = ownerRecordId;
            Type = type;
            ParentId = parentId;
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
