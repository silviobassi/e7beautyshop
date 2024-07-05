using E7BeautyShop.AgendaService.Core.Entities;

namespace E7BeautyShop.AgendaService.Application.Ports.Persistence;

public interface IDayRestRepository
{
    Task<DayRest?> GetByIdAsync(Guid id);
}