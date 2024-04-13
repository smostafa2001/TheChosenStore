using AccountManagement.Application.Contracts.RoleAggregate;
using Common.Domain;
using System.Collections.Generic;

namespace AccountManagement.Domain.RoleAggregate;

public interface IRoleRepository : IRepository<long, Role>
{
    EditRole GetDetails(long id);
    List<RoleViewModel> GetRoles();
}
