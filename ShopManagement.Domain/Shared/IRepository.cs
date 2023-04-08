using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace ShopManagement.Domain.Shared
{
    public interface IRepository<TKey, T> where T : class
    {
        T Get(TKey id);

        List<T> GetAll();

        void Create(T entity);

        bool DoesExist(Expression<Func<T, bool>> expression);

        void Save();
    }
}