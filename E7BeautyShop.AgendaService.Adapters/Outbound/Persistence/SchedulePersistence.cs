using E7BeautyShop.AgendaService.Application.Ports;
using E7BeautyShop.AgendaService.Core.Entities;
using E7BeautyShop.AgendaService.Infra.Context;
using Microsoft.EntityFrameworkCore;

namespace E7BeautyShop.AgendaService.Adapters.Outbound.Persistence;

public class SchedulePersistence(ApplicationDbContext context) : ISchedulePersistencePort
{
    public async Task<Agenda?> GetByIdAsync(Guid id) 
        => await context.Schedules.FirstOrDefaultAsync(s => s.Id == id);

    public async Task<Agenda?> CreateAsync(Agenda calendart)
    {
        context.Add(calendart);
        await context.SaveChangesAsync();
        return calendart;
    }

    public async Task<Agenda?> UpdateAsync(Agenda calendart)
    {
        context.Update(calendart);
        await context.SaveChangesAsync();
        return calendart;
    }

    public async Task<Agenda?> DeleteAsync(Agenda calendart)
    {
        context.Remove(calendart);
        await context.SaveChangesAsync();
        return calendart;
    }
}