using BlogManagement.Infrastructure.EFCore;
using CommentManagement.Application.Contracts.CommentAggregate;
using CommentManagement.Domain.CommentAggregate;
using Framework.Application;
using Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Infrastructure.EFCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CommentManagement.Infrastructure.EFCore.Repository
{
    public class CommentRepository : BaseRepository<long, Comment>, ICommentRepository
    {
        private readonly CommentDbContext _context;
        private readonly BlogDbContext _blogContext;
        private readonly ShopDbContext _shopContext;

        public CommentRepository(CommentDbContext context, ShopDbContext shopContext, BlogDbContext blogContext) : base(context)
        {
            _context = context;
            _shopContext = shopContext;
            _blogContext = blogContext;
        }

        public List<CommentViewModel> Search(CommentSearchModel searchModel)
        {
            var query = _context.Comments.Select(c => new CommentViewModel
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
                if(resultItem.Type == CommentType.Article)
                {
                    resultItem.OwnerName = _blogContext.Articles.FirstOrDefault(a => a.Id == resultItem.OwnerRecordId).Title;
                }
                else if(resultItem.Type == CommentType.Product)
                {
                    resultItem.OwnerName = _shopContext.Products.FirstOrDefault(p => p.Id == resultItem.OwnerRecordId).Name;
                }
            }

            return result;
        }
    }
}
