using E7BeautyShop.Appointment.Application.Ports;
using E7BeautyShop.Appointment.Core;
using E7BeautyShop.Appointment.Infra.Context;
using Microsoft.EntityFrameworkCore;

namespace E7BeautyShop.Appointment.Adapters.Outbound.Persistence;

public class CatalogPersistence(ApplicationDbContext context) : ICatalogPersistencePort
{
    public async Task<IEnumerable<Catalog>> GetCatalogsAsync()
    {
        return await context.Catalogs.ToListAsync();
    }
}