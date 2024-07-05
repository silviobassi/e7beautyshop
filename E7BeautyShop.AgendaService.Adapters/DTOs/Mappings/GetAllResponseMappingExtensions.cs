using E7BeautyShop.AgendaService.Application;
using E7BeautyShop.AgendaService.Core.Entities;

namespace E7BeautyShop.AgendaService.Adapters.DTOs.Mappings;

public static class GetAllResponseMappingExtensions
{
    public static IEnumerable<GetAllAgendaResponse>? ToGetAllAgendaResponsesList(this IEnumerable<Agenda>? agendas)
    {
        return agendas?.Select(a =>
            new GetAllAgendaResponse(a.StartAt, a.EndAt, a.ProfessionalId?.Value, a.Weekday, a.Weekend));
    }
}