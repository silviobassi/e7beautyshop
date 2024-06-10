using System.Linq.Expressions;
using E7BeautyShop.Appointment.Application.Ports;
using E7BeautyShop.Appointment.Infra.Context;
using Microsoft.EntityFrameworkCore;

namespace E7BeautyShop.Appointment.Adapters.Outbound.Persistence;

public class Persistence<T>(ApplicationDbContext context) : IPersistence<T>
    where T : class
{
    protected readonly ApplicationDbContext _context = context;

    public async Task<T?> Get(Expression<Func<T, bool>> predicate)
    {
        return await _context.Set<T>().AsNoTracking().FirstOrDefaultAsync(predicate);
    }

    public T Create(T entity)
    {
        _context.Set<T>().Add(entity);
        return entity;
    }

    public T Update(T entity)
    {
        _context.Set<T>().Update(entity);
        return entity;
    }

    public T Delete(T entity)
    {
        _context.Set<T>().Remove(entity);
        return entity;
    }
}
