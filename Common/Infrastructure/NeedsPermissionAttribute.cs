namespace Common.Infrastructure;

public class NeedsPermissionAttribute(int permission) : Attribute
{
    public int Permission { get; set; } = permission;
}
