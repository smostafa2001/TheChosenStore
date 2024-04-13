using Common.Application;
using System.Collections.Generic;

namespace AccountManagement.Application.Contracts.AccountAggregate;

public interface IAccountApplication
{
    List<AccountViewModel> Accounts { get; }
    OperationResult Register(RegisterAccount command);
    OperationResult Edit(EditAccount command);
    OperationResult ChangePassword(ChangePassword command);
    OperationResult Login(Login command);
    EditAccount GetDetails(long id);
    List<AccountViewModel> Search(AccountSearchModel searchModel);
    void Logout();
}

