using E7BeautyShop.AgendaService.Core.Entities;

namespace E7BeautyShop.AgendaService.Application.Interfaces;

public interface ICreateAgendaUseCase
{
    Task<Agenda?> Execute(Agenda agenda);
}