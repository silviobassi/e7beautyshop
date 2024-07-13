using E7BeautyShop.AgendaService.Application.DTOs;
using E7BeautyShop.AgendaService.Application.DTOs.Requests;
using E7BeautyShop.AgendaService.Application.DTOs.Responses;

namespace E7BeautyShop.AgendaService.Application.Interfaces;

public interface IAgendaCreateUseCase
{
    Task<AgendaResponse?> ExecuteAsync(AgendaRequest projection);
}