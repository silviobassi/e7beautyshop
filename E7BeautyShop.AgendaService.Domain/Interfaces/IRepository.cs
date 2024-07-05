using System.Linq.Expressions;

namespace E7BeautyShop.AgendaService.Domain.Interfaces;

public interface IRepository<T> where T : class
{
    Task<T?> Get(Expression<Func<T, bool>> predicate);
    T Create(T entity);
    T Update(T entity);
    T Delete(T entity);
}