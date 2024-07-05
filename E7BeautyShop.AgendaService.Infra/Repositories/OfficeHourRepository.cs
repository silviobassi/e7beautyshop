using E7BeautyShop.AgendaService.Application.Ports;
using E7BeautyShop.AgendaService.Application.Ports.Persistence;
using E7BeautyShop.AgendaService.Core.Entities;
using E7BeautyShop.AgendaService.Infra.Context;
using Microsoft.EntityFrameworkCore;

namespace E7BeautyShop.AgendaService.Adapters.Outbound.Persistence;

public class OfficeHourRepository(ApplicationDbContext context) : IOfficeHourRepository
{
    public async Task<OfficeHour?> GetBydIdAsync(Guid id) 
        => await context.OfficeHours.FirstOrDefaultAsync(oh => oh.Id == id);
    
    public async Task<OfficeHour?> CreateAsync(OfficeHour officeHour)
    {
        context.Add(officeHour);
        await context.SaveChangesAsync();
        return officeHour;
    }

    public async Task<OfficeHour?> UpdateAsync(OfficeHour officeHour)
    {
        context.Update(officeHour);
        await context.SaveChangesAsync();
        return officeHour;
    }

    public async Task<OfficeHour?> DeleteAsync(OfficeHour officeHour)
    {
        context.Remove(officeHour);
        await context.SaveChangesAsync();
        return officeHour;
    }
}