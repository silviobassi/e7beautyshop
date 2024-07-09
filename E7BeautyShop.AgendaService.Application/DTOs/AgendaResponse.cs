using E7BeautyShop.AgendaService.Domain.Entities;
using E7BeautyShop.AgendaService.Domain.ObjectsValue;
using E7BeautyShop.AgendaService.Domain.ValueObjects;

namespace E7BeautyShop.AgendaService.Application.DTOs;

public record AgendaResponse(
    Guid Id,
    DateTime? StartAt,
    DateTime? EndAt,
    Guid? ProfessionalId,
    Weekday? Weekday,
    Weekend? Weekend,
    IReadOnlyCollection<DayRest> DaysRest);