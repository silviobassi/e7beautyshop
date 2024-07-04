using E7BeautyShop.AgendaService.Application.Ports;
using E7BeautyShop.AgendaService.Application.Ports.Persistence;
using E7BeautyShop.AgendaService.Core.Entities;
using E7BeautyShop.AgendaService.Infra.Context;
using Microsoft.EntityFrameworkCore;

namespace E7BeautyShop.AgendaService.Adapters.Outbound.Persistence;

public class AgendaPersistence(ApplicationDbContext context) : IAgendaPersistencePort
{
    public async Task<Agenda?> GetByIdAsync(Guid id) 
        => await context.Agendas.FirstOrDefaultAsync(s => s.Id == id);

    public async Task<Agenda?> CreateAsync(Agenda agenda)
    {
        context.Add(agenda);
        await context.SaveChangesAsync();
        return agenda;
    }

    public async Task<Agenda?> UpdateAsync(Agenda agenda)
    {
        context.Update(agenda);
        await context.SaveChangesAsync();
        return agenda;
    }

    public async Task<Agenda?> DeleteAsync(Agenda agenda)
    {
        context.Remove(agenda);
        await context.SaveChangesAsync();
        return agenda;
    }
}