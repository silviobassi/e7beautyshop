using E7BeautyShop.AgendaService.Core.Entities;

namespace E7BeautyShop.AgendaService.Application.Ports.UseCases;

public interface ICreateAgendaUseCasePort
{
    Task<Agenda?> CreateAsync(Agenda agenda);
}