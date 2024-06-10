using E7BeautyShop.Appointment.Core;

namespace E7BeautyShop.Appointment.Application.Ports;

public interface ICatalogPersistencePort
{
    Task<IEnumerable<Catalog>> GetCatalogsAsync();
}