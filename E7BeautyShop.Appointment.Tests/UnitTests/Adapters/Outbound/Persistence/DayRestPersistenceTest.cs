using E7BeautyShop.Appointment.Application.Ports;
using E7BeautyShop.Appointment.Core.Entities;
using Microsoft.Extensions.DependencyInjection;

namespace E7BeautyShop.Appointment.Tests.UnitTests.Adapters.Outbound.Persistence;

public class DayRestPersistenceTest
{
    private readonly IDayRestPersistencePort _dayRestPersistence;

    public DayRestPersistenceTest()
    {
        var startup = new TestStartup();
        _dayRestPersistence = startup.ServiceProvider.GetRequiredService<IDayRestPersistencePort>();
    }
    
    [Fact]
    public void Should_Create_DayRest()
    {
        var dayRest = DayRest.Create(DayOfWeek.Sunday);
        var entity = _dayRestPersistence.Create(dayRest);
        Assert.NotNull(entity);
        Assert.Equal(dayRest.Id, entity.Id);
        Assert.Equal(dayRest.DayOnWeek, entity.DayOnWeek);
    }

    [Fact]
    public void Should_Update_DayRest()
    {
        var dayRest = DayRest.Create(DayOfWeek.Sunday);
        var entity = _dayRestPersistence.Update(dayRest);
        Assert.NotNull(entity);
        Assert.Equal(dayRest.Id, entity.Id);
        Assert.Equal(dayRest.DayOnWeek, entity.DayOnWeek);
    }  
    
    [Fact]
    public void Should_Delete_DayRest()
    {
        var dayRest = DayRest.Create(DayOfWeek.Sunday);
        var entity = _dayRestPersistence.Delete(dayRest);
        Assert.NotNull(entity);
        Assert.Equal(dayRest.Id, entity.Id);
        Assert.Equal(dayRest.DayOnWeek, entity.DayOnWeek);
    }
    
    [Fact]
    public async Task Should_NotGet_DayRest()
    {
        var dayRest = DayRest.Create(DayOfWeek.Sunday);
        var entity = _dayRestPersistence.Create(dayRest);
        entity = await _dayRestPersistence.Get( x => x.Id == entity.Id);
        Assert.Null(entity);
    }
}