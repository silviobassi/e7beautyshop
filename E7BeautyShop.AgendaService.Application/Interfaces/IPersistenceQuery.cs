namespace E7BeautyShop.AgendaService.Application.Ports.Persistence;

public interface IPersistenceQuery
{
    Task<IEnumerable<GetAllAgendaResponse>> GetAllAgendasAsync();
}