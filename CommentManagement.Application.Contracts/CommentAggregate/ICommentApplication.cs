using Common.Application;
using System.Collections.Generic;

namespace CommentManagement.Application.Contracts.CommentAggregate;

public interface ICommentApplication
{
    OperationResult Add(AddComment command);
    OperationResult Confirm(long id);
    OperationResult Cancel(long id);
    OperationResult RemoveComment(long id);
    OperationResult Restore(long id);
    OperationResult Review(long id);
    List<CommentViewModel> Search(CommentSearchModel searchModel);
    CommentViewModel GetFullMessage(long id);
}
