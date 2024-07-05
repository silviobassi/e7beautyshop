using E7BeautyShop.AgendaService.Application;
using E7BeautyShop.AgendaService.Application.Ports.Persistence;
using E7BeautyShop.AgendaService.Infra.Data.Connection;
using Microsoft.Data.SqlClient;

namespace E7BeautyShop.AgendaService.Adapters.Outbound.Persistence;

public class PersistenceQuery(IConnectionDb connectionDb) : IPersistenceQuery
{
    public async Task<IEnumerable<GetAllAgendaResponse>> GetAllAgendasAsync()
    {
        var responses = new List<GetAllAgendaResponse>();

        await using var connection = connectionDb.Connection;
        await connection.OpenAsync();
        var command = new SqlCommand(
            "SELECT StartAt, EndAt, Professional_Id, Weekday_StartAt, Weekday_EndAt, Weekend_StartAt, Weekend_EndAt FROM Agendas",
            connection);
        await using var reader = await command.ExecuteReaderAsync();
        while (await reader.ReadAsync())
        {
            // Leia e armazene os dados necessários aqui
            var response = new GetAllAgendaResponse(
                reader.GetDateTime(0), // StartAt
                reader.GetDateTime(1), // EndAt
                reader.GetGuid(2), // ProfessionalId
                (reader.GetTimeSpan(3), reader.GetTimeSpan(4)), // Weekday
                (reader.GetTimeSpan(5), reader.GetTimeSpan(6))); // Weekend
            responses.Add(response);
        }

        return responses;
    }
}