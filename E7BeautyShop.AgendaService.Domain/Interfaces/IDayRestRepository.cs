using E7BeautyShop.AgendaService.Domain.Entities;

namespace E7BeautyShop.AgendaService.Domain.Interfaces;

public interface IDayRestRepository
{
    Task<DayRest?> GetByIdAsync(Guid id);
}