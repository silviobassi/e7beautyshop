using E7BeautyShop.Appointment.Core;
using E7BeautyShop.Appointment.Core.Entities;

namespace E7BeautyShop.Appointment.Application.Ports;

public interface ICatalogPersistencePort 
{
    Task<Catalog?> GetByIdAsync(Guid id);
    Task<Catalog?> CreateAsync(Catalog catalog);
    Task<Catalog?> UpdateAsync(Catalog catalog);
    Task<Catalog?> DeleteAsync(Catalog catalog);
}