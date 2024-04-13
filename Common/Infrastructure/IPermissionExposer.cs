namespace Common.Infrastructure;

public interface IPermissionExposer
{
    Dictionary<string, List<PermissionDto>> Expose();
}
