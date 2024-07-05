using E7BeautyShop.AgendaService.Core.Entities;

namespace E7BeautyShop.AgendaService.Application.Ports.Persistence;

public interface IAgendaRepository
{
    Task<Agenda?> GetByIdAsync(Guid id);
    Task<Agenda?> CreateAsync(Agenda agenda);
    Task<Agenda?> UpdateAsync(Agenda agenda);
    Task<Agenda?> DeleteAsync(Agenda agenda);
    
}