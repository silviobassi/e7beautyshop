using E7BeautyShop.AgendaService.Application.Commands;
using E7BeautyShop.AgendaService.Domain.Entities;
using E7BeautyShop.AgendaService.Domain.Interfaces;
using MediatR;

namespace E7BeautyShop.AgendaService.Application.Handlers;

public class AgendaCreateCommandHandler(IAgendaRepository agendaRepository)
    : IRequestHandler<AgendaCreateCommand, Agenda>
{
    public async Task<Agenda> Handle(AgendaCreateCommand request, CancellationToken cancellationToken)
    {
        var agenda = Agenda.Create(request.StartAt, request.EndAt, request.ProfessionalId!.Value, request.Weekday!,
            request.Weekend!);
        
        ArgumentNullException.ThrowIfNull(nameof(agenda));
        
        if (request.DaysRest.Count > 0)
            agenda.AddDayRests(request.DaysRest);
        
        return await agendaRepository.CreateAsync(agenda);
    }
}