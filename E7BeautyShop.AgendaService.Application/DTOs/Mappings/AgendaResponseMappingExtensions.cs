using E7BeautyShop.AgendaService.Application.DTOs.Responses;
using E7BeautyShop.AgendaService.Domain.Entities;

namespace E7BeautyShop.AgendaService.Application.DTOs.Mappings;

public static class AgendaResponseMappingExtensions
{
    public static IEnumerable<AgendaResponse>? ToAgendasResponse(this IEnumerable<Agenda>? agendas)
    {
        return agendas?.Select(a =>
            new AgendaResponse(a.Id, a.StartAt, a.EndAt, a.ProfessionalId?.Value, a.Weekday, a.Weekend,
                a.DaysRest.ToDaysRestResponse()));
    }

    public static AgendaResponse? ToAgendaResponse(this Agenda? agenda)
    {
        return agenda == null
            ? null
            : new AgendaResponse(agenda.Id, agenda.StartAt, agenda.EndAt, agenda.ProfessionalId?.Value, agenda.Weekday,
                agenda.Weekend, agenda.DaysRest.ToDaysRestResponse());
    }
}