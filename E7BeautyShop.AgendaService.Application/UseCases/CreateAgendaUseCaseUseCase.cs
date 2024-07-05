using E7BeautyShop.AgendaService.Application.Ports.Persistence;
using E7BeautyShop.AgendaService.Application.Ports.UseCases;
using E7BeautyShop.AgendaService.Core.Entities;
using E7BeautyShop.AgendaService.Core.Services;
using Microsoft.Extensions.Logging;

namespace E7BeautyShop.AgendaService.Application.UseCases;

public class CreateAgendaUseCaseUseCase(
    IAgendaPersistencePort agendaPersistencePort,
    ILogger<CreateAgendaUseCaseUseCase> logger) : ICreateAgendaUseCasePort
{
    public async Task<Agenda?> CreateAsync(Agenda agenda)
    {
        try
        {
            var dateAndHourGenerate = new AgendaWorkingHoursGenerator(agenda);
            dateAndHourGenerate.Generate();
            return await agendaPersistencePort.CreateAsync(agenda);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "❌ An error occurred while creating the agenda");
        }

        return null;
    }
}