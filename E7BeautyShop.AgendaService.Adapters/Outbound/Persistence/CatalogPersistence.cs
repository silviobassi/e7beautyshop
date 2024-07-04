using E7BeautyShop.AgendaService.Application.Ports;
using E7BeautyShop.AgendaService.Application.Ports.Persistence;
using E7BeautyShop.AgendaService.Core.Entities;
using E7BeautyShop.AgendaService.Infra.Context;
using Microsoft.EntityFrameworkCore;

namespace E7BeautyShop.AgendaService.Adapters.Outbound.Persistence;

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