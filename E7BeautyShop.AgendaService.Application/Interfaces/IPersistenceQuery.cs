using E7BeautyShop.AgendaService.Application.DTOs;

namespace E7BeautyShop.AgendaService.Application.Interfaces;

public interface IPersistenceQuery
{
    Task<IEnumerable<AgendaResponse>> GetAllAgendasAsync();
}