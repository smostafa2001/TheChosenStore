using AccountManagement.Application.Contracts.RoleAggregate;
using AccountManagement.Domain.RoleAggregate;
using Common.Application;
using Common.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace AccountManagement.Infrastructure.EFCore.Repository;

public class RoleRepository(AccountDbContext context) : BaseRepository<long, Role>(context), IRoleRepository
{
    public EditRole GetDetails(long id)
    {
        var role = context.Roles.Select(r => new EditRole
        {
            Id = r.Id,
            Name = r.Name,
            MappedPermissions = MapPermissions(r.Permissions)
        }).AsNoTracking().FirstOrDefault(r => r.Id == id);

        role.Permissions = role.MappedPermissions.Select(mp => mp.Code).ToList();
        return role;
    }

    private static List<PermissionDto> MapPermissions(List<Permission> permissions) => permissions.Select(p => new PermissionDto
    {
        Code = p.Code,
        Name = p.Name
    }).ToList();
    public List<RoleViewModel> GetRoles() => [.. context.Roles.Select(r => new RoleViewModel
    {
        Id = r.Id,
        Name = r.Name,
        CreationDate = r.CreationDate.ToFarsi()
    })];
}
