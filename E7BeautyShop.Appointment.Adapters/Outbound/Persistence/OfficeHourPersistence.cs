using E7BeautyShop.Appointment.Application.Ports;
using E7BeautyShop.Appointment.Core;
using E7BeautyShop.Appointment.Infra.Context;
using Microsoft.EntityFrameworkCore;

namespace E7BeautyShop.Appointment.Adapters.Outbound.Persistence;

public class OfficeHourPersistence(ApplicationDbContext context): IOfficeHourPersistencePort
{
    public async Task<OfficeHour?> GetByIdAsync(Guid? id)
    {
        return await context.OfficeHours.FirstOrDefaultAsync(c => c.Id == id);
    }
}