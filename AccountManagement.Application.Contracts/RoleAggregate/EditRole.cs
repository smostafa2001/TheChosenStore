using Framework.Infrastructure;
using System.Collections.Generic;

namespace AccountManagement.Application.Contracts.RoleAggregate
{
    public class EditRole:CreateRole
    {
        public long Id { get; set; }
        public List<PermissionDto> MappedPermissions { get; set; }
    }
}
