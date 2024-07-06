using E7BeautyShop.AgendaService.Domain.Entities;

namespace E7BeautyShop.AgendaService.Domain.Interfaces;

public interface IAgendaRepository
{
    Task<Agenda?> GetByIdAsync(Guid id);
    Task<Agenda> CreateAsync(Agenda agenda);
    Task<Agenda?> UpdateAsync(Agenda agenda);
    Task<Agenda?> DeleteAsync(Agenda agenda);
    Task<IEnumerable<Agenda>> GetAgendasAsync();
    
}