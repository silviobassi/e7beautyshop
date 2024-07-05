﻿using E7BeautyShop.AgendaService.Application.DTOs;
using E7BeautyShop.AgendaService.Application.Interfaces;
using E7BeautyShop.AgendaService.Core.Entities;
using E7BeautyShop.AgendaService.Core.Interfaces;
using E7BeautyShop.AgendaService.Core.ObjectsValue;
using Microsoft.Extensions.DependencyInjection;

namespace E7BeautyShop.AgendaService.Tests.IntegrationTests;

public class CreateAgendaUseCaseTest(TestStartup startup) : IClassFixture<TestStartup>
{
    private readonly ICreateAgendaUseCase _createAgendaUseCase =
        startup.ServiceProvider.GetRequiredService<ICreateAgendaUseCase>();

    private readonly IAgendaRepository _agendaPersistence =
        startup.ServiceProvider.GetRequiredService<IAgendaRepository>();

    private readonly IPersistenceQuery _persistenceQuery =
        startup.ServiceProvider.GetRequiredService<IPersistenceQuery>();

    [Fact]
    public async Task Should_CreateAgenda()
    {
        var startAt = new DateTime(2024, 07, 04, 8, 0, 0, 0, DateTimeKind.Utc);
        var endAt = new DateTime(2024, 07, 11, 18, 0, 0, 0, DateTimeKind.Utc);
        ProfessionalId? professionalId = Guid.Parse("e2977a5c-f1d6-46fc-8f7e-f78d0dda568c");
        Weekday weekday = (new TimeSpan(8, 0, 0), new TimeSpan(18, 0, 0));
        Weekend weekend = (new TimeSpan(8, 0, 0), new TimeSpan(12, 0, 0));


        var request = new CreateAgendaRequest(startAt, endAt, professionalId.Value, weekday, weekend,
            [DayRest.Create(DayOfWeek.Monday)]);

        var agendaCreated = await _createAgendaUseCase.Execute(request);
        var currentSchedule = agendaCreated is null ? null : await _agendaPersistence.GetByIdAsync(agendaCreated.Id);
        Assert.NotNull(currentSchedule);
        Assert.Equal(agendaCreated?.StartAt, currentSchedule!.StartAt);
        Assert.Equal(agendaCreated?.EndAt, currentSchedule!.EndAt);
        Assert.Equal(agendaCreated?.ProfessionalId, currentSchedule!.ProfessionalId);
        Assert.Equal(agendaCreated?.Weekday, currentSchedule!.Weekday);
        Assert.Equal(agendaCreated?.Weekend, currentSchedule!.Weekend);

        var agendaDeleted = await _agendaPersistence.DeleteAsync(currentSchedule);
        var agendaDeletedCurrent = await _agendaPersistence.GetByIdAsync(agendaDeleted!.Id);
        Assert.Null(agendaDeletedCurrent);
    }

    [Fact]
    public async Task Should_GetAgendas()
    {
        var startAt = new DateTime(2024, 07, 04, 8, 0, 0, 0, DateTimeKind.Utc);
        var endAt = new DateTime(2024, 07, 11, 18, 0, 0, 0, DateTimeKind.Utc);
        ProfessionalId? professionalId = Guid.Parse("e2977a5c-f1d6-46fc-8f7e-f78d0dda568c");
        Weekday weekday = (new TimeSpan(8, 0, 0), new TimeSpan(18, 0, 0));
        Weekend weekend = (new TimeSpan(8, 0, 0), new TimeSpan(12, 0, 0));

        var request = new CreateAgendaRequest(startAt, endAt, professionalId.Value, weekday, weekend,
            [DayRest.Create(DayOfWeek.Monday)]);

        var agendaCreated = await _createAgendaUseCase.Execute(request);
        var currentSchedule = agendaCreated is null ? null : await _agendaPersistence.GetByIdAsync(agendaCreated.Id);
        Assert.NotNull(currentSchedule);
        Assert.Equal(agendaCreated?.StartAt, currentSchedule!.StartAt);
        Assert.Equal(agendaCreated?.EndAt, currentSchedule!.EndAt);
        Assert.Equal(agendaCreated?.ProfessionalId, currentSchedule!.ProfessionalId);
        Assert.Equal(agendaCreated?.Weekday, currentSchedule!.Weekday);
        Assert.Equal(agendaCreated?.Weekend, currentSchedule!.Weekend);

        var agendas = await _persistenceQuery.GetAllAgendasAsync();
        Assert.Single(agendas);

        var agendaDeleted = await _agendaPersistence.DeleteAsync(currentSchedule);
        var agendaDeletedCurrent = await _agendaPersistence.GetByIdAsync(agendaDeleted!.Id);
        Assert.Null(agendaDeletedCurrent);
    }
}