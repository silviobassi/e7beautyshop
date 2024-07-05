using E7BeautyShop.AgendaService.Domain.Entities;

namespace E7BeautyShop.AgendaService.Domain.Interfaces;

public interface ICatalogRepository 
{
    Task<Catalog?> GetByIdAsync(Guid id);
    Task<Catalog?> CreateAsync(Catalog catalog);
    Task<Catalog?> UpdateAsync(Catalog catalog);
    Task<Catalog?> DeleteAsync(Catalog catalog);
}