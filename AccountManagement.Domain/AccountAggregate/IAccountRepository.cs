using AccountManagement.Application.Contracts.AccountAggregate;
using Framework.Domain;
using System.Collections.Generic;

namespace AccountManagement.Domain.AccountAggregate
{
    public interface IAccountRepository : IRepository<long, Account>
    {
        Account Get(string username);
        EditAccount GetDetails(long id);
        List<AccountViewModel> Search(AccountSearchModel searchModel);
    }
}
