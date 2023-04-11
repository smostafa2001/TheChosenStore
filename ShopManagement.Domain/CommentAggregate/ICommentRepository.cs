using ShopManagement.Domain.Shared;
using System.Collections.Generic;

namespace ShopManagement.Domain.CommentAggregate
{
    public interface ICommentRepository:IRepository<long, Comment>
    {
        List<CommentViewModel> Search(CommentSearchModel searchModel);
    }
}
