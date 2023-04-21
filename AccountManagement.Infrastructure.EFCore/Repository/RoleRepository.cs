using AccountManagement.Application.Contracts.RoleAggregate;
using AccountManagement.Domain.RoleAggregate;
using Framework.Application;
using Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace AccountManagement.Infrastructure.EFCore.Repository
{
    public class RoleRepository : BaseRepository<long, Role>, IRoleRepository
    {
        private readonly AccountDbContext _context;

        public RoleRepository(AccountDbContext context) : base(context) => _context = context;

        public EditRole GetDetails(long id)
        {
            var role = _context.Roles.Select(r => new EditRole
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
        public List<RoleViewModel> GetRoles() => _context.Roles.Select(r => new RoleViewModel
        {
            Id = r.Id,
            Name = r.Name,
            CreationDate = r.CreationDate.ToFarsi()
        }).ToList();
    }
}
