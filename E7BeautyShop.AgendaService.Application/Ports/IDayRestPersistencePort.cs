using E7BeautyShop.AgendaService.Core.Entities;

namespace E7BeautyShop.AgendaService.Application.Ports;

public interface IDayRestPersistencePort
{
    Task<DayRest?> GetByIdAsync(Guid id);
}