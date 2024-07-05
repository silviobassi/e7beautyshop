using E7BeautyShop.AgendaService.Application.Ports.Persistence;
using E7BeautyShop.AgendaService.Application.Ports.UseCases;
using E7BeautyShop.AgendaService.Core.Entities;
using E7BeautyShop.AgendaService.Core.ObjectsValue;
using Microsoft.Extensions.DependencyInjection;

namespace E7BeautyShop.AgendaService.Tests.IntegrationTests;

public class CreateAgendaUseCaseTest(TestStartup startup) : IClassFixture<TestStartup>
{
    private readonly ICreateAgendaUseCasePort _createAgendaUseCase =
        startup.ServiceProvider.GetRequiredService<ICreateAgendaUseCasePort>();
    private readonly IAgendaPersistencePort _agendaPersistence =
        startup.ServiceProvider.GetRequiredService<IAgendaPersistencePort>();
    
    private readonly IPersistenceQueryPort _persistenceQueryPort =
        startup.ServiceProvider.GetRequiredService<IPersistenceQueryPort>();

    [Fact]
    public async Task Should_CreateAgenda()
    {
        var startAt = new DateTime(2024, 07, 04, 8,0,0,0, DateTimeKind.Utc);
        var endAt = new DateTime(2024, 07, 11, 18,0,0,0, DateTimeKind.Utc);
        ProfessionalId? professionalId = Guid.Parse("e2977a5c-f1d6-46fc-8f7e-f78d0dda568c");
        Weekday weekday = (new TimeSpan(8,0,0), new TimeSpan(18,0,0));
        Weekend weekend = (new TimeSpan(8,0,0), new TimeSpan(12,0,0));
        
        var agenda = Agenda.Create(startAt, endAt, professionalId, weekday, weekend);
        agenda.AddDayRest(DayRest.Create(DayOfWeek.Monday));
        
        var agendaCreated = await _createAgendaUseCase.Execute(agenda);
        var currentSchedule = agendaCreated is null ? null : await _agendaPersistence.GetByIdAsync(agendaCreated.Id);
        Assert.NotNull(currentSchedule);
        Assert.Equal(agendaCreated?.StartAt, currentSchedule!.StartAt);
        Assert.Equal(agendaCreated?.EndAt, currentSchedule!.EndAt);
        Assert.Equal(agendaCreated?.ProfessionalId, currentSchedule!.ProfessionalId);
        Assert.Equal(agendaCreated?.Weekday, currentSchedule!.Weekday);
        Assert.Equal(agendaCreated?.Weekend, currentSchedule!.Weekend);
        Assert.Equal(agendaCreated?.OfficeHours.Count, currentSchedule!.OfficeHours.Count);
        Assert.Equal(agendaCreated?.DaysRest.Count, currentSchedule!.DaysRest.Count);
        
        var agendaDeleted = await _agendaPersistence.DeleteAsync(currentSchedule);
        var agendaDeletedCurrent = await _agendaPersistence.GetByIdAsync(agendaDeleted!.Id);
        Assert.Null(agendaDeletedCurrent);
    }

    [Fact]
    public async Task Should_GetAgendas()
    {
        var startAt = new DateTime(2024, 07, 04, 8,0,0,0, DateTimeKind.Utc);
        var endAt = new DateTime(2024, 07, 11, 18,0,0,0, DateTimeKind.Utc);
        ProfessionalId? professionalId = Guid.Parse("e2977a5c-f1d6-46fc-8f7e-f78d0dda568c");
        Weekday weekday = (new TimeSpan(8,0,0), new TimeSpan(18,0,0));
        Weekend weekend = (new TimeSpan(8,0,0), new TimeSpan(12,0,0));
        
        var agenda = Agenda.Create(startAt, endAt, professionalId, weekday, weekend);
        agenda.AddDayRest(DayRest.Create(DayOfWeek.Monday));
        
        var agendaCreated = await _createAgendaUseCase.Execute(agenda);
        var currentSchedule = agendaCreated is null ? null : await _agendaPersistence.GetByIdAsync(agendaCreated.Id);
        Assert.NotNull(currentSchedule);
        Assert.Equal(agendaCreated?.StartAt, currentSchedule!.StartAt);
        Assert.Equal(agendaCreated?.EndAt, currentSchedule!.EndAt);
        Assert.Equal(agendaCreated?.ProfessionalId, currentSchedule!.ProfessionalId);
        Assert.Equal(agendaCreated?.Weekday, currentSchedule!.Weekday);
        Assert.Equal(agendaCreated?.Weekend, currentSchedule!.Weekend);
        Assert.Equal(agendaCreated?.OfficeHours.Count, currentSchedule!.OfficeHours.Count);
        Assert.Equal(agendaCreated?.DaysRest.Count, currentSchedule!.DaysRest.Count);
        
        var agendas = await _persistenceQueryPort.GetAllAgendasAsync();
        Assert.Single(agendas);
        
        var agendaDeleted = await _agendaPersistence.DeleteAsync(currentSchedule);
        var agendaDeletedCurrent = await _agendaPersistence.GetByIdAsync(agendaDeleted!.Id);
        Assert.Null(agendaDeletedCurrent);
    }
}