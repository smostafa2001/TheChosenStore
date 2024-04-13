using Common.Domain;
using System.Collections.Generic;

namespace CommentManagement.Domain.CommentAggregate;

public class Comment(string name, string email, string website, string message, long ownerRecordId, byte type, long? parentId) : EntityBase
{
    public string Name { get; private set; } = name;
    public string Email { get; private set; } = email;
    public string Website { get; private set; } = website;
    public string Message { get; private set; } = message;
    public bool IsConfirmed { get; private set; }
    public bool IsCanceled { get; private set; }
    public long OwnerRecordId { get; private set; } = ownerRecordId;
    public byte Type { get; private set; } = type;
    public long? ParentId { get; private set; } = parentId;
    public Comment Parent { get; set; }
    public List<Comment> Children { get; private set; }

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
