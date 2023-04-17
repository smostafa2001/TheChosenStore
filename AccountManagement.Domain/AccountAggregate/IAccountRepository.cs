using AccountManagement.Application.Contracts.AccountAggregate;
using Framework.Domain;
using System.Collections.Generic;

namespace AccountManagement.Domain.AccountAggregate
{
    public interface IAccountRepository : IRepository<long, Account>
    {
        EditAccount GetDetails(long id);
        List<AccountViewModel> Search(AccountSearchModel searchModel);
    }
}
