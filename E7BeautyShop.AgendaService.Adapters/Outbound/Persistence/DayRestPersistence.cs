using E7BeautyShop.AgendaService.Application.Ports;
using E7BeautyShop.AgendaService.Core.Entities;
using E7BeautyShop.AgendaService.Infra.Context;
using Microsoft.EntityFrameworkCore;

namespace E7BeautyShop.AgendaService.Adapters.Outbound.Persistence;

public class DayRestPersistence(ApplicationDbContext context) : IDayRestPersistencePort
{
    public async Task<DayRest?> GetByIdAsync(Guid id)
        => await context.DaysRest.FirstOrDefaultAsync(c => c.Id == id);
}