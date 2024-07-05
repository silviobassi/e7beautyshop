using E7BeautyShop.AgendaService.Application.DTOs;

namespace E7BeautyShop.AgendaService.Application.Interfaces;

public interface ICreateAgendaUseCase
{
    Task<AgendaResponse?> Execute(CreateAgendaRequest agendaRequest);
}