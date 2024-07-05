using E7BeautyShop.AgendaService.Core.ObjectsValue;

namespace E7BeautyShop.AgendaService.Application;

public record GetAllAgendaResponse(
    DateTime? StartAt,
    DateTime? EndAt,
    Guid? ProfessionalId,
    Weekday? Weekday,
    Weekend? Weekend);