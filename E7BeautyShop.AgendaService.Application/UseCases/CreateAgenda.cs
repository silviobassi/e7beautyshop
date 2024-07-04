using E7BeautyShop.AgendaService.Application.Ports.Persistence;
using E7BeautyShop.AgendaService.Application.Ports.UseCases;
using E7BeautyShop.AgendaService.Core.Entities;

namespace E7BeautyShop.AgendaService.Application.UseCases;

public class CreateAgenda(IAgendaPersistencePort persistencePort) : ICreateAgendaPort
{
    public async Task<Agenda?> CreateAsync(Agenda agenda)
    {
        return await persistencePort.CreateAsync(agenda);
    }
}