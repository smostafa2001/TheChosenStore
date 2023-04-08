using DiscountManagement.Domain.Shared;
using System.Collections.Generic;

namespace DiscountManagement.Domain.ColleagueDiscountAggregate
{
    public interface IColleagueDiscountRepository : IRepository<long, ColleagueDiscount>
    {
        EditColleagueDiscount GetDetails(long id);

        List<ColleagueDiscountViewModel> Search(ColleagueDiscountSearchModel searchModel);
    }
}