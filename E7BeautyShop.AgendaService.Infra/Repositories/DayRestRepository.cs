using E7BeautyShop.AgendaService.Domain.Entities;
using E7BeautyShop.AgendaService.Domain.Interfaces;
using E7BeautyShop.AgendaService.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace E7BeautyShop.AgendaService.Infra.Repositories;

public class DayRestRepository(ApplicationDbContext context) : IDayRestRepository
{
    public async Task<DayRest?> GetByIdAsync(Guid id)
        => await context.DaysRest.FirstOrDefaultAsync(c => c.Id == id);
}