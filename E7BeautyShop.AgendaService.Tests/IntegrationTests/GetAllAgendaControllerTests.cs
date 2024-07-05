﻿using System.Net;
using E7BeautyShop.AgendaService.Application;
using E7BeautyShop.AgendaService.Application.Ports.Persistence;
using E7BeautyShop.AgendaService.Application.Ports.UseCases;
using E7BeautyShop.AgendaService.Core.Entities;
using E7BeautyShop.AgendaService.Core.ObjectsValue;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace E7BeautyShop.AgendaService.Tests.IntegrationTests;

public class GetAllAgendaIntegrationTest : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;
    private readonly ICreateAgendaUseCasePort _createAgendaUseCase;
    private readonly IAgendaPersistencePort _agendaPersistence;

    public GetAllAgendaIntegrationTest(WebApplicationFactory<Program> factory)
    {
        var startup = new TestStartup();
        _factory = factory;
        _createAgendaUseCase = startup.ServiceProvider.GetRequiredService<ICreateAgendaUseCasePort>();
        _agendaPersistence = startup.ServiceProvider.GetRequiredService<IAgendaPersistencePort>();
    }

    [Fact]
    public async Task GetAllAgendas_ReturnsOkResult_WithAgendas()
    {
        var agendaCreated = await SetupAgendaAsync();
        Assert.NotNull(agendaCreated);

        var client = _factory.CreateClient();
        
        var response = await client.GetAsync("/api/GetAllAgenda");
        response.EnsureSuccessStatusCode();
        var responseString = await response.Content.ReadAsStringAsync();
        var agendas = JsonConvert.DeserializeObject<List<GetAllAgendaResponse>>(responseString);

        Assert.Single(agendas);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        
        await _agendaPersistence.DeleteAsync(agendaCreated);
    }

    
    private async Task<Agenda?> SetupAgendaAsync()
    {
        var startAt = new DateTime(2024, 07, 04, 8, 0, 0, 0, DateTimeKind.Utc);
        var endAt = new DateTime(2024, 07, 11, 18, 0, 0, 0, DateTimeKind.Utc);
        var professionalId = Guid.Parse("e2977a5c-f1d6-46fc-8f7e-f78d0dda568c");
        var weekday = (new TimeSpan(8, 0, 0), new TimeSpan(18, 0, 0));
        var weekend = (new TimeSpan(8, 0, 0), new TimeSpan(12, 0, 0));

        var agenda = Agenda.Create(startAt, endAt, professionalId, weekday, weekend);
        agenda.AddDayRest(DayRest.Create(DayOfWeek.Monday));

        return await _createAgendaUseCase.Execute(agenda);
    }

}