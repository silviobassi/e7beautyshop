using E7BeautyShop.AgendaService.Application.DTOs;
using E7BeautyShop.AgendaService.Application.Interfaces;
using E7BeautyShop.AgendaService.Infra.Data.Connection;
using Microsoft.Data.SqlClient;

namespace E7BeautyShop.AgendaService.Infra.Repositories;

public class PersistenceQuery(IConnectionDb connectionDb) : IPersistenceQuery
{
    public async Task<IEnumerable<AgendaResponse>> GetAllAgendasAsync()
    {
        var responses = new List<AgendaResponse>();

        await using var connection = connectionDb.Connection;
        await connection.OpenAsync();
        var command = new SqlCommand(
            "SELECT Id, StartAt, EndAt, Professional_Id, Weekday_StartAt, Weekday_EndAt, Weekend_StartAt, Weekend_EndAt FROM Agendas",
            connection);
        await using var reader = await command.ExecuteReaderAsync();
        while (await reader.ReadAsync())
        {
            // Leia e armazene os dados necessários aqui
            var response = new AgendaResponse(
                reader.GetGuid(0), // Id
                reader.GetDateTime(1), // StartAt
                reader.GetDateTime(2), // EndAt
                reader.GetGuid(3), // ProfessionalId
                (reader.GetTimeSpan(4), reader.GetTimeSpan(5)), // Weekday
                (reader.GetTimeSpan(6), reader.GetTimeSpan(7))); // Weekend
            responses.Add(response);
        }

        return responses;
    }
}