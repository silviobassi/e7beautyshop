using E7BeautyShop.AgendaService.Core.Entities;

namespace E7BeautyShop.AgendaService.Application.DTOs.Mappings;

public static class GetAllResponseMappingExtensions
{
    public static IEnumerable<AgendaResponse>? ToGetAllAgendaResponsesList(this IEnumerable<Agenda>? agendas)
    {
        return agendas?.Select(a =>
            new AgendaResponse(a.Id, a.StartAt, a.EndAt, a.ProfessionalId?.Value, a.Weekday, a.Weekend));
    }

    public static AgendaResponse? ToAgendaResponse(this Agenda? agenda)
    {
        return agenda == null
            ? null
            : new AgendaResponse(agenda.Id, agenda.StartAt, agenda.EndAt, agenda.ProfessionalId?.Value, agenda.Weekday,
                agenda.Weekend);
    }

    public static Agenda ToAgenda(this CreateAgendaRequest? agendaRequest)
    {
        if (agendaRequest is null)
            return null!;
        return Agenda.Create(agendaRequest.StartAt!, agendaRequest.EndAt!, agendaRequest.ProfessionalId!.Value,
            agendaRequest.Weekday!, agendaRequest.Weekend!);
    }
}