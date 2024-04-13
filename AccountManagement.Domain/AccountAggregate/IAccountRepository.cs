using AccountManagement.Application.Contracts.AccountAggregate;
using Common.Domain;
using System.Collections.Generic;

namespace AccountManagement.Domain.AccountAggregate;

public interface IAccountRepository : IRepository<long, Account>
{
    List<AccountViewModel> Accounts { get; }
    Account Get(string username);
    Account GetWithRole(string username);
    EditAccount GetDetails(long id);
    List<AccountViewModel> Search(AccountSearchModel searchModel);
}
