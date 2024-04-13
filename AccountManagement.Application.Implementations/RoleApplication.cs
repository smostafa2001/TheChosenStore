using AccountManagement.Application.Contracts.RoleAggregate;
using AccountManagement.Domain.RoleAggregate;
using Common.Application;
using System.Collections.Generic;

namespace AccountManagement.Application.Implementations;

public class RoleApplication(IRoleRepository repository) : IRoleApplication
{
    public OperationResult Create(CreateRole command)
    {
        OperationResult operation = new OperationResult();
        if (repository.Exists(r => r.Name == command.Name))
            return operation.Failed(ApplicationMessages.DuplicatedRecord);

        Role role = new Role(command.Name, []);
        repository.Create(role);
        repository.Save();
        return operation.Succeeded();
    }

    public OperationResult Edit(EditRole command)
    {
        OperationResult operation = new OperationResult();
        Role role = repository.Get(command.Id);
        if (role is null)
            return operation.Failed(ApplicationMessages.RecordNotFound);

        if (repository.Exists(r => r.Name == command.Name && r.Id != command.Id))
            return operation.Failed(ApplicationMessages.DuplicatedRecord);

        var permissions = new List<Permission>();
        command?.Permissions?.ForEach(code => permissions?.Add(new Permission(code)));

        role.Edit(command.Name, permissions);
        repository.Save();
        return operation.Succeeded();
    }

    public EditRole GetDetails(long id) => repository.GetDetails(id);
    public List<RoleViewModel> GetRoles() => repository.GetRoles();
}
