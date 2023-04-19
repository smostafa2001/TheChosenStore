using AccountManagement.Application.Contracts.RoleAggregate;
using Framework.Domain;
using System.Collections.Generic;

namespace AccountManagement.Domain.RoleAggregate
{
    public interface IRoleRepository:IRepository<long, Role>
    {
        EditRole GetDetails(long id);
        List<RoleViewModel> GetRoles();
    }
}
