using Microsoft.EntityFrameworkCore;
using ShopManagement.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace ShopManagement.Infrastructure.EFCore.Shared
{
    public class BaseRepository<TKey, T> : IRepository<TKey, T> where T : class
    {
        private readonly DbContext _context;

        public BaseRepository(DbContext context) => _context = context;

        public void Create(T entity) => _context.Add(entity);

        public bool DoesExist(Expression<Func<T, bool>> expression) => _context.Set<T>().Any(expression);

        public T Get(TKey id) => _context.Find<T>(id);

        public List<T> GetAll() => _context.Set<T>().ToList();

        public void Save() => _context.SaveChanges();
    }
}