using E7BeautyShop.AgendaService.Application.DTOs;
using E7BeautyShop.AgendaService.Application.DTOs.Mappings;
using E7BeautyShop.AgendaService.Application.DTOs.Requests;
using E7BeautyShop.AgendaService.Application.DTOs.Responses;
using E7BeautyShop.AgendaService.Application.Interfaces;
using E7BeautyShop.AgendaService.Domain.Entities;
using E7BeautyShop.AgendaService.Domain.Interfaces;
using E7BeautyShop.AgendaService.Domain.Services;
using E7BeautyShop.AgendaService.Domain.ValueObjects;

namespace E7BeautyShop.AgendaService.Application.UseCases;

public class AgendaCreateUseCase(IAgendaRepository agendaRepository)
    : IAgendaCreateUseCase
{
    public async Task<AgendaResponse?> ExecuteAsync(AgendaRequest projection)
    {
        try
        {
            Weekday weekday = (projection.WeekdayStartAt, projection.WeekdayEndAt);
            Weekend weekend = (projection.WeekendStartAt, projection.WeekendEndAt);

            var agenda = Agenda.Create(projection.StartAt, projection.EndAt, projection.ProfessionalId!.Value, weekday, weekend);

            foreach (var dayRest in projection.DaysRest!)
            {
                agenda.AddDayRest(DayRest.Create(dayRest.DayOnWeek));
            }

            var timesGenerator = new AgendaWorkingHoursGenerator(agenda);
            timesGenerator.Generate();
            var agendaCreated = await agendaRepository.CreateAsync(agenda);

            return agendaCreated?.ToAgendaResponse();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}