using E7BeautyShop.Appointment.Application.Ports;
using E7BeautyShop.Appointment.Core.Entities;
using E7BeautyShop.Appointment.Infra.Context;
using Microsoft.EntityFrameworkCore;

namespace E7BeautyShop.Appointment.Adapters.Outbound.Persistence;

public class SchedulePersistence(ApplicationDbContext context) : ISchedulePersistencePort
{
    public async Task<Schedule?> GetByIdAsync(Guid id) 
        => await context.Schedules.FirstOrDefaultAsync(s => s.Id == id);

    public async Task<Schedule?> CreateAsync(Schedule schedule)
    {
        context.Add(schedule);
        await context.SaveChangesAsync();
        return schedule;
    }

    public async Task<Schedule?> UpdateAsync(Schedule schedule)
    {
        context.Update(schedule);
        await context.SaveChangesAsync();
        return schedule;
    }

    public async Task<Schedule?> DeleteAsync(Schedule schedule)
    {
        context.Remove(schedule);
        await context.SaveChangesAsync();
        return schedule;
    }
}