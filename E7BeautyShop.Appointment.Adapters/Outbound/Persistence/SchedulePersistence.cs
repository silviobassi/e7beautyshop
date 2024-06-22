using E7BeautyShop.Appointment.Application.Ports;
using E7BeautyShop.Appointment.Core.Entities;
using E7BeautyShop.Appointment.Infra.Context;
using Microsoft.EntityFrameworkCore;

namespace E7BeautyShop.Appointment.Adapters.Outbound.Persistence;

public class SchedulePersistence(ApplicationDbContext context) : ISchedulePersistencePort
{
    public async Task<Core.Entities.Agenda?> GetByIdAsync(Guid id) 
        => await context.Schedules.FirstOrDefaultAsync(s => s.Id == id);

    public async Task<Core.Entities.Agenda?> CreateAsync(Core.Entities.Agenda calendart)
    {
        context.Add(calendart);
        await context.SaveChangesAsync();
        return calendart;
    }

    public async Task<Core.Entities.Agenda?> UpdateAsync(Core.Entities.Agenda calendart)
    {
        context.Update(calendart);
        await context.SaveChangesAsync();
        return calendart;
    }

    public async Task<Core.Entities.Agenda?> DeleteAsync(Core.Entities.Agenda calendart)
    {
        context.Remove(calendart);
        await context.SaveChangesAsync();
        return calendart;
    }
}