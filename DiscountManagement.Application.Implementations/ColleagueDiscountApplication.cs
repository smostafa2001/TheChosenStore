using Common.Application;
using DiscountManagement.Application.Contracts.ColleagueDiscountAggregate;
using DiscountManagement.Domain.ColleagueDiscountAggregate;
using System.Collections.Generic;

namespace DiscountManagement.Application.Implementations;

public class ColleagueDiscountApplication(IColleagueDiscountRepository repository) : IColleagueDiscountApplication
{
    public OperationResult Define(DefineColleagueDiscount command)
    {
        var operation = new OperationResult();
        if (repository.Exists(cd => cd.ProductId == command.ProductId && cd.DiscountRate == command.DiscountRate))
            return operation.Failed(ApplicationMessages.DuplicatedRecord);

        var colleagueDiscount = new ColleagueDiscount(command.ProductId, command.DiscountRate);
        repository.Create(colleagueDiscount);
        repository.Save();
        return operation.Succeeded();
    }

    public OperationResult Edit(EditColleagueDiscount command)
    {
        var operation = new OperationResult();
        var colleagueDiscount = repository.Get(command.Id);
        if (colleagueDiscount is null)
            return operation.Failed(ApplicationMessages.RecordNotFound);
        if (repository.Exists(cd => cd.ProductId == command.ProductId && cd.DiscountRate == command.DiscountRate && cd.Id == command.Id))
            return operation.Failed(ApplicationMessages.DuplicatedRecord);

        colleagueDiscount.Edit(command.ProductId, command.DiscountRate);
        repository.Save();
        return operation.Succeeded();
    }

    public EditColleagueDiscount GetDetails(long id) => repository.GetDetails(id);

    public OperationResult Remove(long id)
    {
        var operation = new OperationResult();
        var colleagueDiscount = repository.Get(id);
        if (colleagueDiscount is null)
            return operation.Failed(ApplicationMessages.RecordNotFound);

        colleagueDiscount.Remove();
        repository.Save();
        return operation.Succeeded();
    }

    public OperationResult Restore(long id)
    {
        var operation = new OperationResult();
        var colleagueDiscount = repository.Get(id);
        if (colleagueDiscount is null)
            return operation.Failed(ApplicationMessages.RecordNotFound);

        colleagueDiscount.Restore();
        repository.Save();
        return operation.Succeeded();
    }

    public List<ColleagueDiscountViewModel> Search(ColleagueDiscountSearchModel searchModel) => repository.Search(searchModel);
}
