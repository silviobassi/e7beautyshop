using E7BeautyShop.AgendaService.Application.Commands;
using E7BeautyShop.AgendaService.Domain.Entities;
using E7BeautyShop.AgendaService.Domain.Interfaces;
using E7BeautyShop.AgendaService.Domain.ObjectsValue;
using E7BeautyShop.AgendaService.Domain.Services;
using E7BeautyShop.AgendaService.Domain.ValueObjects;
using MediatR;

namespace E7BeautyShop.AgendaService.Application.Handlers;

public class AgendaCreateCommandHandler(IAgendaRepository agendaRepository)
    : IRequestHandler<AgendaCreateCommand, Agenda>
{
    public async Task<Agenda> Handle(AgendaCreateCommand request, CancellationToken cancellationToken)
    {
        Weekday weekday = (request.WeekdayStartAt, request.WeekdayEndAt);
        Weekend weekend = (request.WeekendStartAt, request.WeekendEndAt);
        
        var agenda = Agenda.Create(
            request.StartAt, 
            request.EndAt, 
            request.ProfessionalId!.Value, 
            weekday,
            weekend);

        foreach (var dayRest in request.DaysRest!)
        {
            agenda.AddDayRest(DayRest.Create(dayRest.DayOnWeek));
        }
        
        var workingHoursGenerator = new AgendaWorkingHoursGenerator(agenda);
        workingHoursGenerator.Generate();
        
        ArgumentNullException.ThrowIfNull(nameof(agenda));

        return await agendaRepository.CreateAsync(agenda);
    }
}