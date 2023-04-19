using AccountManagement.Application.Contracts.RoleAggregate;
using AccountManagement.Domain.RoleAggregate;
using Framework.Application;
using System.Collections.Generic;

namespace AccountManagement.Application.Implementations
{
    public class RoleApplication : IRoleApplication
    {
        private readonly IRoleRepository _repository;

        public RoleApplication(IRoleRepository repository) => _repository = repository;

        public OperationResult Create(CreateRole command)
        {
            OperationResult operation = new OperationResult();
            if (_repository.DoesExist(r => r.Name == command.Name))
                return operation.Failed(ApplicationMessages.DuplicatedRecord);

            Role role = new Role(command.Name);
            _repository.Create(role);
            _repository.Save();
            return operation.Succeeded();
        }

        public OperationResult Edit(EditRole command)
        {
            OperationResult operation = new OperationResult();
            Role role = _repository.Get(command.Id);
            if (role is null)
                return operation.Failed(ApplicationMessages.RecordNotFound);

            if (_repository.DoesExist(r => r.Name == command.Name && r.Id != command.Id))
                return operation.Failed(ApplicationMessages.DuplicatedRecord);

            role.Edit(command.Name);
            _repository.Save();
            return operation.Succeeded();
        }

        public EditRole GetDetails(long id) => _repository.GetDetails(id);
        public List<RoleViewModel> GetRoles() => _repository.GetRoles();
    }
}
