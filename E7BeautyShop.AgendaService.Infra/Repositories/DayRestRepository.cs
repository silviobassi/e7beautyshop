using E7BeautyShop.AgendaService.Application.Ports;
using E7BeautyShop.AgendaService.Application.Ports.Persistence;
using E7BeautyShop.AgendaService.Core.Entities;
using E7BeautyShop.AgendaService.Infra.Context;
using Microsoft.EntityFrameworkCore;

namespace E7BeautyShop.AgendaService.Adapters.Outbound.Persistence;

public class DayRestRepository(ApplicationDbContext context) : IDayRestRepository
{
    public async Task<DayRest?> GetByIdAsync(Guid id)
        => await context.DaysRest.FirstOrDefaultAsync(c => c.Id == id);
}