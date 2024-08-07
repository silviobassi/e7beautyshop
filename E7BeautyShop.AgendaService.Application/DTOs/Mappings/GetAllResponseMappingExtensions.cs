﻿using E7BeautyShop.AgendaService.Domain.Entities;

namespace E7BeautyShop.AgendaService.Application.DTOs.Mappings;

public static class GetAllResponseMappingExtensions
{
    public static IEnumerable<AgendaResponse>? ToGetAllAgendas(this IEnumerable<Agenda>? agendas)
    {
        return agendas?.Select(a =>
            new AgendaResponse(a.Id, a.StartAt, a.EndAt, a.ProfessionalId?.Value, a.Weekday, a.Weekend, a.DaysRest));
    }

    public static AgendaResponse? ToAgendaResponse(this Agenda? agenda)
    {
        return agenda == null
            ? null
            : new AgendaResponse(agenda.Id, agenda.StartAt, agenda.EndAt, agenda.ProfessionalId?.Value, agenda.Weekday,
                agenda.Weekend, agenda.DaysRest);
    }
}