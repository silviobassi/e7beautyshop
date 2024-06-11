using E7BeautyShop.Appointment.Application.Ports;
using E7BeautyShop.Appointment.Core.Entities;
using E7BeautyShop.Appointment.Infra.Context;
using Microsoft.EntityFrameworkCore;

namespace E7BeautyShop.Appointment.Adapters.Outbound.Persistence;

public class DayRestPersistence(ApplicationDbContext context) : IDayRestPersistencePort
{
    public async Task<DayRest?> GetByIdAsync(Guid id)
        => await context.DaysRest.FirstOrDefaultAsync(c => c.Id == id);
}