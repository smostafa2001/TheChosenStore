using CommentManagement.Application.Contracts.CommentAggregate;
using CommentManagement.Domain.CommentAggregate;
using Common.Application;
using System.Collections.Generic;

namespace CommentManagement.Application.Implementations;

public class CommentApplication(ICommentRepository commentRepository) : ICommentApplication
{
    public OperationResult Add(AddComment command)
    {
        OperationResult operation = new OperationResult();
        Comment comment = new Comment(
            command.Name, command.Email, command.Website, 
            command.Message, command.OwnerRecordId, 
            command.Type, command.ParentId
            );

        commentRepository.Create(comment);
        commentRepository.Save();
        return operation.Succeeded();
    }

    public OperationResult Cancel(long id)
    {
        OperationResult operationResult = new OperationResult();
        Comment comment = commentRepository.Get(id);
        if (comment is null)
            return operationResult.Failed(ApplicationMessages.RecordNotFound);

        comment.Cancel();
        commentRepository.Save();
        return operationResult.Succeeded();
    }

    public OperationResult Confirm(long id)
    {
        OperationResult operationResult = new OperationResult();
        Comment comment = commentRepository.Get(id);
        if (comment is null)
            return operationResult.Failed(ApplicationMessages.RecordNotFound);

        comment.Confirm();
        commentRepository.Save();
        return operationResult.Succeeded();
    }

    public CommentViewModel GetFullMessage(long id)
    {
        var comment = commentRepository.Get(id);
        CommentViewModel model = new CommentViewModel
        {
            Name = comment.Name,
            Message = comment.Message
        };

        return model;
    }

    public OperationResult RemoveComment(long id)
    {
        OperationResult operationResult = new OperationResult();
        Comment comment = commentRepository.Get(id);
        if (comment is null)
            return operationResult.Failed(ApplicationMessages.RecordNotFound);

        comment.Remove();
        commentRepository.Save();
        return operationResult.Succeeded();
    }

    public OperationResult Restore(long id)
    {
        OperationResult operationResult = new OperationResult();
        Comment comment = commentRepository.Get(id);
        if (comment is null)
            return operationResult.Failed(ApplicationMessages.RecordNotFound);

        comment.Restore();
        commentRepository.Save();
        return operationResult.Succeeded();
    }

    public OperationResult Review(long id)
    {
        OperationResult operationResult = new OperationResult();
        Comment comment = commentRepository.Get(id);
        if (comment is null)
            return operationResult.Failed(ApplicationMessages.RecordNotFound);

        comment.Review();
        commentRepository.Save();
        return operationResult.Succeeded();
    }

    public List<CommentViewModel> Search(CommentSearchModel searchModel) => commentRepository.Search(searchModel);
}