using DiscountManagement.Application.Shared;
using DiscountManagement.Domain.ColleagueDiscountAggregate;
using System.Collections.Generic;

namespace DiscountManagement.Application.Contracts
{
    public interface IColleagueDiscountApplication
    {
        OperationResult Define(DefineColleagueDiscount command);

        OperationResult Edit(EditColleagueDiscount command);

        OperationResult Remove(long id);

        OperationResult Restore(long id);

        EditColleagueDiscount GetDetails(long id);

        List<ColleagueDiscountViewModel> Search(ColleagueDiscountSearchModel searchModel);
    }
}