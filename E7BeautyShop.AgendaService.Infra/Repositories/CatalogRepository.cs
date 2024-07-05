using E7BeautyShop.AgendaService.Domain.Entities;
using E7BeautyShop.AgendaService.Domain.Interfaces;
using E7BeautyShop.AgendaService.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace E7BeautyShop.AgendaService.Infra.Repositories;

public class CatalogRepository(ApplicationDbContext context) : ICatalogRepository
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