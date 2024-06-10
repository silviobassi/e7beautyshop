using E7BeautyShop.Appointment.Application.Ports;
using E7BeautyShop.Appointment.Core;
using E7BeautyShop.Appointment.Infra.Context;
using Microsoft.EntityFrameworkCore;

namespace E7BeautyShop.Appointment.Adapters.Outbound.Persistence;

public class OfficeHourPersistence(ApplicationDbContext context): IOfficeHourPersistencePort
{
    public async Task<IEnumerable<OfficeHour>> GetOfficeHoursAsync()
    {
        return await context.OfficeHours.ToListAsync();
    }
}