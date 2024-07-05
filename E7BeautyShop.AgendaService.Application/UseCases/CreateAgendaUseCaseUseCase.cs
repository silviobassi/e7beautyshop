using E7BeautyShop.AgendaService.Application.DTOs;
using E7BeautyShop.AgendaService.Application.DTOs.Mappings;
using E7BeautyShop.AgendaService.Application.Interfaces;
using E7BeautyShop.AgendaService.Core.Entities;
using E7BeautyShop.AgendaService.Core.Interfaces;
using E7BeautyShop.AgendaService.Core.Services;
using Microsoft.Extensions.Logging;

namespace E7BeautyShop.AgendaService.Application.UseCases;

public class CreateAgendaUseCaseUseCase(
    IAgendaRepository agendaRepository,
    ILogger<CreateAgendaUseCaseUseCase> logger) : ICreateAgendaUseCase
{
    public async Task<AgendaResponse?> Execute(CreateAgendaRequest agendaRequest)
    {
        try
        {
            var agenda = agendaRequest.ToAgenda();
            agenda.AddDayRests(agendaRequest.DaysRest);
            var dateAndHourGenerate = new AgendaWorkingHoursGenerator(agenda);
            dateAndHourGenerate.Generate();
            var agendaCreated = await agendaRepository.CreateAsync(agenda);
            return agendaCreated?.ToAgendaResponse();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "❌ An error occurred while creating the agenda");
        }

        return null;
    }
}