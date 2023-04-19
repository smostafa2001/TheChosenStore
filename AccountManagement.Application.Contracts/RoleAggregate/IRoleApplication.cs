using Framework.Application;
using System.Collections.Generic;

namespace AccountManagement.Application.Contracts.RoleAggregate
{
    public interface IRoleApplication
    {
        OperationResult Create(CreateRole command);
        OperationResult Edit(EditRole command);
        EditRole GetDetails(long id);
        List<RoleViewModel> GetRoles();
    }
}
