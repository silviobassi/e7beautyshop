using E7BeautyShop.AgendaService.Domain.Entities;
using E7BeautyShop.AgendaService.Domain.ObjectsValue;
using MediatR;

namespace E7BeautyShop.AgendaService.Application.Commands;

public abstract record AgendaCommand(
    DateTime StartAt,
    DateTime EndAt,
    Guid? ProfessionalId,
    Weekday? Weekday,
    Weekend? Weekend,
    List<DayRest> DaysRest) : IRequest<Agenda>
{
}