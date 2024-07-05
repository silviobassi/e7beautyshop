using E7BeautyShop.AgendaService.Core.Entities;

namespace E7BeautyShop.AgendaService.Application.Ports.Persistence;

public interface ICatalogRepository 
{
    Task<Catalog?> GetByIdAsync(Guid id);
    Task<Catalog?> CreateAsync(Catalog catalog);
    Task<Catalog?> UpdateAsync(Catalog catalog);
    Task<Catalog?> DeleteAsync(Catalog catalog);
}