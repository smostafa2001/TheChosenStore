using AccountManagement.Domain.AccountAggregate;
using Framework.Domain;
using System.Collections.Generic;

namespace AccountManagement.Domain.RoleAggregate
{
    public class Role : EntityBase
    {
        public string Name { get; private set; }
        public List<Account> Accounts { get; private set; }

        public Role(string name) => Name = name;

        public void Edit(string name) => Name = name;
    }
}
