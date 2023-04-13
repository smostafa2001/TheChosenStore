using CommentManagement.Application.Contracts.CommentAggregate;
using CommentManagement.Domain.CommentAggregate;
using Framework.Application;
using Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CommentManagement.Infrastructure.EFCore.Repository
{
    public class CommentRepository : BaseRepository<long, Comment>, ICommentRepository
    {
        private readonly CommentDbContext _context;

        public CommentRepository(CommentDbContext context) : base(context) => _context = context;

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

            return query.OrderByDescending(c => c.Id).ToList();
        }
    }
}
