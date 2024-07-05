using E7BeautyShop.AgendaService.Core.ObjectsValue;

namespace E7BeautyShop.AgendaService.Application.DTOs;

public record AgendaResponse(
    Guid Id,
    DateTime? StartAt,
    DateTime? EndAt,
    Guid? ProfessionalId,
    Weekday? Weekday,
    Weekend? Weekend);