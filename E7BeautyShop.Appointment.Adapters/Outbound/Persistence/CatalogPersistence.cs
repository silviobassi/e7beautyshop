using E7BeautyShop.Appointment.Application.Ports;
using E7BeautyShop.Appointment.Core.Entities;
using E7BeautyShop.Appointment.Infra.Context;
using Microsoft.EntityFrameworkCore;

namespace E7BeautyShop.Appointment.Adapters.Outbound.Persistence;

public class CatalogPersistence(ApplicationDbContext context) : ICatalogPersistencePort
{
    public async Task<Catalog?> GetByIdAsync(Guid id)
        => await context.Catalogs.FirstOrDefaultAsync(c => c.Id == id)!;
    
    public async Task<Catalog?> CreateAsync(Catalog catalog)
    {
        context.Catalogs.Add(catalog);
        await context.SaveChangesAsync();
        return catalog;
    }

    public async Task<Catalog?> UpdateAsync(Catalog catalog)
    {
        context.Update(catalog);
        await context.SaveChangesAsync();
        return catalog;
    }

    public async Task<Catalog?> DeleteAsync(Catalog catalog)
    {
        context.Remove(catalog);
        await context.SaveChangesAsync();
        return catalog;
    }
}