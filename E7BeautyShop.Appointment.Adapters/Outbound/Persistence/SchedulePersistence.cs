using E7BeautyShop.Appointment.Application.Ports;
using E7BeautyShop.Appointment.Core;
using E7BeautyShop.Appointment.Infra.Context;
using Microsoft.EntityFrameworkCore;

namespace E7BeautyShop.Appointment.Adapters.Outbound.Persistence;

public class SchedulePersistence(ApplicationDbContext context) : ISchedulePersistencePort
{
    public async Task<Schedule?> GetByIdAsync(Guid? id)
    {
        return await context.Schedules.FirstOrDefaultAsync(c => c.Id == id);
    }
}