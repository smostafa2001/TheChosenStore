using AccountManagement.Application.Contracts.AccountAggregate;
using AccountManagement.Domain.AccountAggregate;
using AccountManagement.Domain.RoleAggregate;
using Common.Application;
using System.Collections.Generic;
using System.Linq;

namespace AccountManagement.Application.Implementations;

public class AccountApplication(IAccountRepository repository, IPasswordHasher hasher, IFileUploader fileUploader, IAuthHelper authHelper, IRoleRepository roleRepository) : IAccountApplication
{
    public List<AccountViewModel> Accounts => repository.Accounts;

    public OperationResult ChangePassword(ChangePassword command)
    {
        OperationResult operation = new OperationResult();
        var account = repository.Get(command.Id);
        if (account is null)
            return operation.Failed(ApplicationMessages.RecordNotFound);

        if (hasher.Check(account.Password, command.Password).verified)
            return operation.Failed(ApplicationMessages.DuplicatedPassword);

        if (command.Password != command.RePassword)
            return operation.Failed(ApplicationMessages.PasswordsNotMatch);

        var password = hasher.Hash(command.Password);
        account.ChangePassword(password);
        repository.Save();
        return operation.Succeeded();
    }

    public OperationResult Register(RegisterAccount command)
    {
        OperationResult operation = new OperationResult();
        if (repository.Exists(a => a.Username == command.Username || a.Mobile == command.Mobile))
            return operation.Failed(ApplicationMessages.DuplicatedRecord);

        if (command.Password != command.RePassword)
            return operation.Failed(ApplicationMessages.PasswordsNotMatch);

        var password = hasher.Hash(command.Password);
        var path = $"ProfilePhotos";
        var picturePath = fileUploader.Upload(command.ProfilePhoto, path);
        var account = new Account(command.Fullname, command.Username, password, command.Mobile, command.RoleId, picturePath);
        repository.Create(account);
        repository.Save();
        return operation.Succeeded(ApplicationMessages.SuccessfulRegister);
    }

    public OperationResult Edit(EditAccount command)
    {
        OperationResult operation = new OperationResult();
        var account = repository.Get(command.Id);
        if (account is null)
            return operation.Failed(ApplicationMessages.RecordNotFound);

        if (repository.Exists(a => (a.Username == command.Username || a.Mobile == command.Mobile) && a.Id != command.Id))
            return operation.Failed(ApplicationMessages.DuplicatedRecord);
        var path = $"ProfilePhotos";
        var picturePath = fileUploader.Upload(command.ProfilePhoto, path);
        account.Edit(command.Fullname, command.Username, command.Mobile, command.RoleId, picturePath);
        repository.Save();
        return operation.Succeeded();
    }

    public EditAccount GetDetails(long id) => repository.GetDetails(id);
    public OperationResult Login(Login command)
    {
        OperationResult operation = new OperationResult();
        Account account = repository.GetWithRole(command.Username);
        if (account is null)
            return operation.Failed(ApplicationMessages.WrongUserPass);

        var (verified, needsUpgrade) = hasher.Check(account.Password, command.Password);
        if (!verified)
            return operation.Failed(ApplicationMessages.WrongUserPass);

        var permissions = roleRepository.Get(account.RoleId).Permissions.Select(p => p.Code).ToList();
        var authModel = new AuthViewModel
        {
            Id = account.Id,
            Fullname = account.Fullname,
            RoleId = account.RoleId,
            Role = account.Role.Name,
            Username = account.Username,
            ProfilePhoto = account.ProfilePhoto,
            Permissions = permissions
        };
        authHelper.SignIn(authModel);
        return operation.Succeeded();
    }

    public void Logout() => authHelper.SignOut();
    public List<AccountViewModel> Search(AccountSearchModel searchModel) => repository.Search(searchModel);
}
