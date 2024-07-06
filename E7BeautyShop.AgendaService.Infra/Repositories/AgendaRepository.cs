using E7BeautyShop.AgendaService.Domain.Entities;
using E7BeautyShop.AgendaService.Domain.Interfaces;
using E7BeautyShop.AgendaService.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace E7BeautyShop.AgendaService.Infra.Repositories;

public class AgendaRepository(ApplicationDbContext context) : IAgendaRepository
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

    public async Task<IEnumerable<Agenda>> GetAgendasAsync()
    {
        return await context.Agendas.AsNoTracking().Include(a => a.DaysRest).ToListAsync();
    }
}