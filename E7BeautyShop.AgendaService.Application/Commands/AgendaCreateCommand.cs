using E7BeautyShop.AgendaService.Domain.Entities;
using E7BeautyShop.AgendaService.Domain.ObjectsValue;

namespace E7BeautyShop.AgendaService.Application.Commands;

public record AgendaCreateCommand(
    DateTime StartAt,
    DateTime EndAt,
    Guid? ProfessionalId,
    Weekday? Weekday,
    Weekend? Weekend,
    List<DayRest> DaysRest) : AgendaCommand(StartAt, EndAt, ProfessionalId, Weekday, Weekend, DaysRest)
{
}