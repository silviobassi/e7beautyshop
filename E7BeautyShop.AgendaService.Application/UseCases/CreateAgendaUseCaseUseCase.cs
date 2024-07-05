using E7BeautyShop.AgendaService.Application.Interfaces;
using E7BeautyShop.AgendaService.Application.Ports.Persistence;
using E7BeautyShop.AgendaService.Core.Entities;
using E7BeautyShop.AgendaService.Core.Services;
using Microsoft.Extensions.Logging;

namespace E7BeautyShop.AgendaService.Application.UseCases;

public class CreateAgendaUseCaseUseCase(
    IAgendaRepository agendaRepository,
    ILogger<CreateAgendaUseCaseUseCase> logger) : ICreateAgendaUseCase
{
    public async Task<Agenda?> Execute(Agenda agenda)
    {
        try
        {
            var dateAndHourGenerate = new AgendaWorkingHoursGenerator(agenda);
            dateAndHourGenerate.Generate();
            return await agendaRepository.CreateAsync(agenda);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "❌ An error occurred while creating the agenda");
        }

        return null;
    }
}