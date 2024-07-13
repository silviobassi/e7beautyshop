using E7BeautyShop.AgendaService.Application.DTOs.Responses;
using E7BeautyShop.AgendaService.Domain.Entities;

namespace E7BeautyShop.AgendaService.Application.DTOs.Mappings;

public static class DaysRestResponseMappingExtensions
{
    public static IEnumerable<DayRestResponse>? ToDaysRestResponse(this IEnumerable<DayRest>? daysRest)
    {
        return daysRest?.Select(d => new DayRestResponse {DayOnWeek = d.DayOnWeek});
    }
    
    public static DayRestResponse? ToDayRestResponse(this DayRest? dayRest)
    {
        return dayRest == null
            ? null
            :  new DayRestResponse {DayOnWeek = dayRest.DayOnWeek};
    }
}