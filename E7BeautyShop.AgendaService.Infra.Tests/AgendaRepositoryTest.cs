using E7BeautyShop.AgendaService.Domain.Entities;
using E7BeautyShop.AgendaService.Domain.Interfaces;
using E7BeautyShop.AgendaService.Domain.ObjectsValue;
using E7BeautyShop.AgendaService.Domain.Services;
using E7BeautyShop.AgendaService.Domain.ValueObjects;
using Microsoft.Extensions.DependencyInjection;

namespace E7BeautyShop.AgendaService.Infra.Tests;

public class AgendaRepositoryTest(TestStartup startup) : IClassFixture<TestStartup>
{
    private readonly IAgendaRepository _agendaRepository =
        startup.ServiceProvider.GetRequiredService<IAgendaRepository>();

    [Fact]
    public async Task Should_CreateAsync_Agenda_ReturnsAgenda()
    {
        var agenda = CreateAgenda();
        agenda.AddDayRest(DayRest.Create(DayOfWeek.Sunday));
        
        var timesGenerator = new AgendaWorkingHoursGenerator(agenda);
        timesGenerator.Generate();
        
        var result = await _agendaRepository!.CreateAsync(agenda);
        Assert.NotNull(result);
        
        var agendaInDb = await _agendaRepository.GetByIdAsync(result.Id);
        Assert.NotNull(agendaInDb);
    }
    
    [Fact]
    public async Task Should_UpdateAsync_Agenda_ReturnsAgenda()
    {
        var startAt = new DateTime(2024, 7, 9, 8, 0, 0, DateTimeKind.Utc);
        var endAt = new DateTime(2024, 7, 11, 8, 0, 0, DateTimeKind.Utc);
        ProfessionalId? professionalId = Guid.Parse("594b9762-28c4-4e74-bfe9-77b9a1ffc365");
        Weekday weekday = (new TimeSpan(8, 0, 0), new TimeSpan(18, 0, 0));
        Weekend weekend = (new TimeSpan(8, 0, 0), new TimeSpan(12, 0, 0));

        var agenda = Agenda.Create(startAt, endAt, professionalId, weekday, weekend);

        agenda.AddDayRest(DayRest.Create(DayOfWeek.Sunday));

        var timesGenerator = new AgendaWorkingHoursGenerator(agenda);
        timesGenerator.Generate();

        var result = await _agendaRepository!.CreateAsync(agenda);

        Assert.NotNull(result);

        var agendaInDb = await _agendaRepository.GetByIdAsync(result.Id);
        Assert.NotNull(agendaInDb);

        var endAtUpdated = new DateTime(2024, 7, 10, 12, 0, 0, DateTimeKind.Utc);

        agendaInDb.Update(agendaInDb.Id, startAt, endAtUpdated, professionalId, weekday, weekend);
        var agendaUpdated = await _agendaRepository.UpdateAsync(agendaInDb);

        Assert.NotNull(agendaUpdated);
        Assert.Equal(agendaUpdated.EndAt, endAtUpdated);

        var agendaInDbUpdated = await _agendaRepository.GetByIdAsync(agendaUpdated.Id);
        Assert.NotNull(agendaInDbUpdated);
        Assert.Equal(agendaInDbUpdated.EndAt, endAtUpdated);
    }

    [Fact]
    public async Task Should_DeleteAsync_Agenda_ReturnsAgenda()
    {
        var agenda = CreateAgenda();
        agenda.AddDayRest(DayRest.Create(DayOfWeek.Sunday));
        
        var timesGenerator = new AgendaWorkingHoursGenerator(agenda);
        timesGenerator.Generate();
        
        var result = await _agendaRepository!.CreateAsync(agenda);
        Assert.NotNull(result);
        
        var agendaInDb = await _agendaRepository.GetByIdAsync(result.Id);
        Assert.NotNull(agendaInDb);
        
        await _agendaRepository.DeleteAsync(agendaInDb);
        var agendaInDbUpdated = await _agendaRepository.GetByIdAsync(agendaInDb.Id);
        Assert.Null(agendaInDbUpdated);
    }
    
    private static Agenda CreateAgenda()
    {
        var startAt = new DateTime(2024, 7, 9, 8, 0, 0, DateTimeKind.Utc);
        var endAt = new DateTime(2024, 7, 11, 8, 0, 0, DateTimeKind.Utc);
        ProfessionalId? professionalId = Guid.Parse("594b9762-28c4-4e74-bfe9-77b9a1ffc365");
        Weekday weekday = (new TimeSpan(8, 0, 0), new TimeSpan(18, 0, 0));
        Weekend weekend = (new TimeSpan(8, 0, 0), new TimeSpan(12, 0, 0));

        var agenda = Agenda.Create(startAt, endAt, professionalId, weekday, weekend);
        return agenda;
    }
}