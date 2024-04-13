using System.Linq.Expressions;

namespace Common.Domain;

public interface IRepository<TKey, T> where T : class
{
    T Get(TKey id);

    List<T> GetAll();

    void Create(T entity);

    bool Exists(Expression<Func<T, bool>> expression);

    void Save();
}
