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
        
        var result = await _createAgendaUseCase.CreateAsync(agenda);
        var currentSchedule = result is null ? null : await _agendaPersistence.GetByIdAsync(result.Id);
        Assert.NotNull(currentSchedule);
        
    }
}