using System.Linq.Expressions;

namespace E7BeautyShop.AgendaService.Application.Ports;

public interface IPersistence<T> where T : class
{
    Task<T?> Get(Expression<Func<T, bool>> predicate);
    T Create(T entity);
    T Update(T entity);
    T Delete(T entity);
}