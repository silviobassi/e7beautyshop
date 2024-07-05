using E7BeautyShop.AgendaService.Domain.Entities;
using E7BeautyShop.AgendaService.Domain.Interfaces;
using E7BeautyShop.AgendaService.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace E7BeautyShop.AgendaService.Infra.Repositories;

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