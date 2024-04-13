using BlogManagement.Infrastructure.EFCore;
using CommentManagement.Application.Contracts.CommentAggregate;
using CommentManagement.Domain.CommentAggregate;
using Common.Application;
using Common.Infrastructure;
using ShopManagement.Infrastructure.EFCore;
using System.Collections.Generic;
using System.Linq;

namespace CommentManagement.Infrastructure.EFCore.Repository;

public class CommentRepository(CommentDbContext context, ShopDbContext shopContext, BlogDbContext blogContext) : BaseRepository<long, Comment>(context), ICommentRepository
{
    public List<CommentViewModel> Search(CommentSearchModel searchModel)
    {
        var query = context.Comments.Select(c => new CommentViewModel
        {
            Id = c.Id,
            Name = c.Name,
            Email = c.Email,
            Website = c.Website,
            Message = c.Message,
            OwnerRecordId = c.OwnerRecordId,
            Type = c.Type,
            CommentDate = c.CreationDate.ToFarsi(),
            IsCanceled = c.IsCanceled,
            IsConfirmed = c.IsConfirmed
        });

        if (!string.IsNullOrWhiteSpace(searchModel.Name))
            query = query.Where(c => c.Name.Contains(searchModel.Name));

        if (!string.IsNullOrWhiteSpace(searchModel.Email))
            query = query.Where(c => c.Email.Contains(searchModel.Email));

        var result = query.OrderByDescending(c => c.Id).ToList();
        foreach (var resultItem in result)
        {
            if (resultItem.Type == CommentType.Article)
            {
                resultItem.OwnerName = blogContext.Articles.FirstOrDefault(a => a.Id == resultItem.OwnerRecordId).Title;
            }
            else if (resultItem.Type == CommentType.Product)
            {
                resultItem.OwnerName = shopContext.Products.FirstOrDefault(p => p.Id == resultItem.OwnerRecordId).Name;
            }
        }

        return result;
    }
}
