using Common.Domain;
using DiscountManagement.Application.Contracts.ColleagueDiscountAggregate;
using System.Collections.Generic;

namespace DiscountManagement.Domain.ColleagueDiscountAggregate;

public interface IColleagueDiscountRepository : IRepository<long, ColleagueDiscount>
{
    EditColleagueDiscount GetDetails(long id);

    List<ColleagueDiscountViewModel> Search(ColleagueDiscountSearchModel searchModel);
}