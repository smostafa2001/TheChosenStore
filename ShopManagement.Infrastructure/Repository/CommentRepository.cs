using Framework.Application;
using Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Application.Contracts.CommentAggregate;
using ShopManagement.Domain.CommentAggregate;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ShopManagement.Infrastructure.EFCore.Repository
{
    public class CommentRepository : BaseRepository<long, Comment>, ICommentRepository
    {
        private readonly ShopDbContext _context;

        public CommentRepository(ShopDbContext context) : base(context) => _context = context;

        public List<CommentViewModel> Search(CommentSearchModel searchModel)
        {
            var query = _context.Comments.Include(c => c.Product).Select(c => new CommentViewModel
            {
                Id = c.Id,
                Name = c.Name,
                Email = c.Email,
                Message = c.Message,
                ProductId = c.ProductId,
                ProductName = c.Product.Name,
                CommentDate = c.CreationDate.ToFarsi(),
                IsCanceled = c.IsCanceled,
                IsConfirmed = c.IsConfirmed
            });

            if (!string.IsNullOrWhiteSpace(searchModel.Name))
                query = query.Where(c => c.Name.Contains(searchModel.Name));

            if (!string.IsNullOrWhiteSpace(searchModel.Email))
                query = query.Where(c => c.Email.Contains(searchModel.Email));

            return query.OrderByDescending(c => c.Id).ToList();
        }
    }
}
