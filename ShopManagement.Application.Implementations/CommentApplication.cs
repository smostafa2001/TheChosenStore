using Framework.Application;
using ShopManagement.Application.Contracts.CommentAggregate;
using ShopManagement.Domain.CommentAggregate;
using System.Collections.Generic;

namespace ShopManagement.Application.Implementations
{
    public class CommentApplication : ICommentApplication
    {
        private readonly ICommentRepository _commentRepository;

        public CommentApplication(ICommentRepository commentRepository) => _commentRepository = commentRepository;

        public OperationResult Add(AddComment command)
        {
            OperationResult operation = new OperationResult();
            Comment comment = new Comment
            (
                command.Name,
                command.Email,
                command.Message,
                command.ProductId
            );

            _commentRepository.Create(comment);
            _commentRepository.Save();
            return operation.Succeeded();
        }

        public OperationResult Cancel(long id)
        {
            OperationResult operationResult = new OperationResult();
            Comment comment = _commentRepository.Get(id);
            if (comment is null)
                return operationResult.Failed(ApplicationMessages.RecordNotFound);

            comment.Cancel();
            _commentRepository.Save();
            return operationResult.Succeeded();
        }

        public OperationResult Confirm(long id)
        {
            OperationResult operationResult = new OperationResult();
            Comment comment = _commentRepository.Get(id);
            if (comment is null)
                return operationResult.Failed(ApplicationMessages.RecordNotFound);

            comment.Confirm();
            _commentRepository.Save();
            return operationResult.Succeeded();
        }

        public OperationResult RemoveComment(long id)
        {
            OperationResult operationResult = new OperationResult();
            Comment comment = _commentRepository.Get(id);
            if (comment is null)
                return operationResult.Failed(ApplicationMessages.RecordNotFound);

            comment.Remove();
            _commentRepository.Save();
            return operationResult.Succeeded();
        }

        public OperationResult Restore(long id)
        {
            OperationResult operationResult = new OperationResult();
            Comment comment = _commentRepository.Get(id);
            if (comment is null)
                return operationResult.Failed(ApplicationMessages.RecordNotFound);

            comment.Restore();
            _commentRepository.Save();
            return operationResult.Succeeded();
        }

        public OperationResult Review(long id)
        {
            OperationResult operationResult = new OperationResult();
            Comment comment = _commentRepository.Get(id);
            if (comment is null)
                return operationResult.Failed(ApplicationMessages.RecordNotFound);

            comment.Review();
            _commentRepository.Save();
            return operationResult.Succeeded();
        }

        public List<CommentViewModel> Search(CommentSearchModel searchModel) => _commentRepository.Search(searchModel);
    }
}
