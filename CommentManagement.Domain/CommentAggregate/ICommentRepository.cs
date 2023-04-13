using CommentManagement.Application.Contracts.CommentAggregate;
using Framework.Domain;
using System.Collections.Generic;

namespace CommentManagement.Domain.CommentAggregate
{
    public interface ICommentRepository : IRepository<long, Comment>
    {
        List<CommentViewModel> Search(CommentSearchModel searchModel);
    }
}
