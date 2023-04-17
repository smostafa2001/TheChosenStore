using AccountManagement.Application.Contracts.AccountAggregate;
using AccountManagement.Domain.AccountAggregate;
using Framework.Application;
using System.Collections.Generic;

namespace AccountManagement.Application.Implementations
{
    public class AccountApplication : IAccountApplication
    {
        private readonly IAccountRepository _repository;
        private readonly IPasswordHasher _hasher;
        private readonly IFileUploader _fileUploader;

        public AccountApplication(IAccountRepository repository, IPasswordHasher hasher, IFileUploader fileUploader)
        {
            _repository = repository;
            _hasher = hasher;
            _fileUploader = fileUploader;
        }

        public OperationResult ChangePassword(ChangePassword command)
        {
            OperationResult operation = new OperationResult();
            var account = _repository.Get(command.Id);
            if (account is null)
                return operation.Failed(ApplicationMessages.RecordNotFound);


            if(_hasher.Check(account.Password, command.Password).verified)
            //if (account.Password == command.Password)
                return operation.Failed(ApplicationMessages.DuplicatedPassword);

            if (command.Password != command.RePassword)
                return operation.Failed(ApplicationMessages.PasswordsNotMatch);

            var password = _hasher.Hash(command.Password);
            account.ChangePassword(password);
            _repository.Save();
            return operation.Succeeded();
        }

        public OperationResult Create(CreateAccount command)
        {
            OperationResult operation = new OperationResult();
            if (_repository.DoesExist(a => a.Username == command.Username || a.Mobile == command.Mobile))
                return operation.Failed(ApplicationMessages.DuplicatedRecord);

            if (command.Password != command.RePassword)
                return operation.Failed(ApplicationMessages.PasswordsNotMatch);

            var password = _hasher.Hash(command.Password);
            var path = $"ProfilePhotos";
            var picturePath = _fileUploader.Upload(command.ProfilePhoto, path);
            var account = new Account(command.Fullname, command.Username, password, command.Mobile, command.RoleId, picturePath);
            _repository.Create(account);
            _repository.Save();
            return operation.Succeeded();
        }

        public OperationResult Edit(EditAccount command)
        {
            OperationResult operation = new OperationResult();
            var account = _repository.Get(command.Id);
            if (account is null)
                return operation.Failed(ApplicationMessages.RecordNotFound);

            if (_repository.DoesExist(a => (a.Username == command.Username || a.Mobile == command.Mobile) && a.Id != command.Id))
                return operation.Failed(ApplicationMessages.DuplicatedRecord);
                var path = $"ProfilePhotos";
            var picturePath = _fileUploader.Upload(command.ProfilePhoto, path);
            account.Edit(command.Fullname, command.Username, command.Mobile, command.RoleId, picturePath);
            _repository.Save();
            return operation.Succeeded();
        }

        public EditAccount GetDetails(long id) => _repository.GetDetails(id);
        public List<AccountViewModel> Search(AccountSearchModel searchModel) => _repository.Search(searchModel);
    }
}
