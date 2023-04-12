using Framework.Domain;
using ShopManagement.Application.Contracts.CommentAggregate;
using System.Collections.Generic;

namespace ShopManagement.Domain.CommentAggregate
{
    public interface ICommentRepository : IRepository<long, Comment>
    {
        List<CommentViewModel> Search(CommentSearchModel searchModel);
    }
}
