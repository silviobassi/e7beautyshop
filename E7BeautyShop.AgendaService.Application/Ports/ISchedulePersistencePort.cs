using E7BeautyShop.AgendaService.Core.Entities;

namespace E7BeautyShop.AgendaService.Application.Ports;

public interface ISchedulePersistencePort
{
    Task<Agenda?> GetByIdAsync(Guid id);
    Task<Agenda?> CreateAsync(Agenda calendart);
    Task<Agenda?> UpdateAsync(Agenda calendart);
    Task<Agenda?> DeleteAsync(Agenda calendart);
}