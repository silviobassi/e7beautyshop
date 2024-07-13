using E7BeautyShop.AgendaService.Domain.ValueObjects;

namespace E7BeautyShop.AgendaService.Application.DTOs.Responses;

public record AgendaResponse(
    Guid Id,
    DateTime? StartAt,
    DateTime? EndAt,
    Guid? ProfessionalId,
    Weekday? Weekday,
    Weekend? Weekend,
    IEnumerable<DayRestResponse>? DaysRest);
    
