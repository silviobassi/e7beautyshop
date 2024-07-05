using E7BeautyShop.AgendaService.Core.Entities;
using E7BeautyShop.AgendaService.Core.ObjectsValue;

namespace E7BeautyShop.AgendaService.Application.DTOs;

public record CreateAgendaRequest(
    DateTime StartAt,
    DateTime EndAt,
    Guid? ProfessionalId,
    Weekday? Weekday,
    Weekend? Weekend,
    List<DayRest> DaysRest);