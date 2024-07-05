namespace E7BeautyShop.AgendaService.Application.Ports.Persistence;

public interface IPersistenceQueryPort
{
    Task<IEnumerable<GetAllAgendaResponse>> GetAllAgendasAsync();
}