using AccountManagement.Domain.AccountAggregate;
using Framework.Domain;
using System.Collections.Generic;

namespace AccountManagement.Domain.RoleAggregate
{
    public class Role : EntityBase
    {
        public string Name { get; private set; }
        public List<Permission> Permissions { get; private set; }
        public List<Account> Accounts { get; private set; }
        protected Role() { }
        public Role(string name, List<Permission> permissions)
        {
            Name = name;
            Permissions = permissions;
        }

        public void Edit(string name, List<Permission> permissions)
        {
            Name = name;
            Permissions = permissions;
        }
    }
}
