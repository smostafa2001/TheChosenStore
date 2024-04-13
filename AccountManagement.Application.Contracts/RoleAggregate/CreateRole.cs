using System.Collections.Generic;

namespace AccountManagement.Application.Contracts.RoleAggregate;

public class CreateRole
{
    public string Name { get; set; }
    public List<int> Permissions { get; set; }
}
