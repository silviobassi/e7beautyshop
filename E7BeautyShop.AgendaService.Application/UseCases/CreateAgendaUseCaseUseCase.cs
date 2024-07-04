/*using E7BeautyShop.AgendaService.Application.Ports.Persistence;
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
            var dateAndHourGenerate = new AgendaWorkingHoursGenerator();
            dateAndHourGenerate.Generate(agenda);

            var startDate = dateAndHourGenerate.CurrentDate.Date;
            var startTime = dateAndHourGenerate.StartTime!.Value;
            var endTime = dateAndHourGenerate.EndTime!.Value;

            var currentTime = startDate.AddHours(startTime.Hours).AddMinutes(startTime.Minutes);
            var endDateTime = startDate.AddHours(endTime.Hours).AddMinutes(endTime.Minutes);

            while (currentTime < endDateTime)
            {
                var newOfficeHour = OfficeHour.Create(currentTime, 30);
                agenda.AddOfficeHour(newOfficeHour);
                currentTime = newOfficeHour.PlusDuration();
            }
            
            return await agendaPersistencePort.CreateAsync(agenda);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "❌ An error occurred while creating the agenda");
        }

        return null;
    }

    private static void AddOfficeHoursToAgenda(Agenda agenda, AgendaWorkingHoursGenerator dateAndHourGenerate)
    {
        var startDate = dateAndHourGenerate.CurrentDate.Date;
        var startTime = dateAndHourGenerate.StartTime!.Value;
        var endTime = dateAndHourGenerate.EndTime!.Value;

        var currentTime = startDate.AddHours(startTime.Hours).AddMinutes(startTime.Minutes);
        var endDateTime = startDate.AddHours(endTime.Hours).AddMinutes(endTime.Minutes);

        while (currentTime < endDateTime)
        {
            var newOfficeHour = OfficeHour.Create(currentTime, 30);
            agenda.AddOfficeHour(newOfficeHour);
            currentTime = newOfficeHour.PlusDuration();
        }
    }
}*/