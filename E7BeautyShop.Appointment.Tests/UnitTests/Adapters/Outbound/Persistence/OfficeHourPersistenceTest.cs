using E7BeautyShop.Appointment.Application.Ports;
using E7BeautyShop.Appointment.Core.Entities;
using Microsoft.Extensions.DependencyInjection;

namespace E7BeautyShop.Appointment.Tests.UnitTests.Adapters.Outbound.Persistence;

public class OfficeHourPersistenceTest
{
    
    private  readonly IOfficeHourPersistencePort _officeHourPersistence;
    
    public OfficeHourPersistenceTest()
    {
        var startup = new TestStartup();
        _officeHourPersistence = startup.ServiceProvider.GetRequiredService<IOfficeHourPersistencePort>();
    }

    [Fact]
    public void Should_Create_OfficeHour()
    {
        var dataAndHour = DateTime.Now.AddDays(1);
        const int duration = 20;
        var officeHour = OfficeHour.Create(dataAndHour, duration);
        var entity = _officeHourPersistence.Create(officeHour);
        Assert.NotNull(entity);
        Assert.Equal(officeHour.Id, entity.Id);
        Assert.Equal(officeHour.DateAndHour, entity.DateAndHour);
        Assert.Equal(officeHour.Duration, entity.Duration);
    }
    
    [Fact]
    public void Should_Update_OfficeHour()
    {
        var dataAndHour = DateTime.Now.AddDays(1);
        const int duration = 20;
        var officeHour = OfficeHour.Create(dataAndHour, duration);
        var entity = _officeHourPersistence.Update(officeHour);
        Assert.NotNull(entity);
        Assert.Equal(officeHour.Id, entity.Id);
        Assert.Equal(officeHour.DateAndHour, entity.DateAndHour);
        Assert.Equal(officeHour.Duration, entity.Duration);
    }
    
    [Fact]
    public void Should_Delete_OfficeHour()
    {
        var dataAndHour = DateTime.Now.AddDays(1);
        const int duration = 20;
        var officeHour = OfficeHour.Create(dataAndHour, duration);
        var entity = _officeHourPersistence.Delete(officeHour);
        Assert.NotNull(entity);
        Assert.Equal(officeHour.Id, entity.Id);
        Assert.Equal(officeHour.DateAndHour, entity.DateAndHour);
        Assert.Equal(officeHour.Duration, entity.Duration);
    }
    
     
    [Fact]
    public async Task Should_NotGet_OfficeHour()
    {
        var dataAndHour = DateTime.Now.AddDays(1);
        const int duration = 20;
        var officeHour = OfficeHour.Create(dataAndHour, duration);
        var entity = _officeHourPersistence.Create(officeHour);
        entity = await _officeHourPersistence.Get( x => x.Id == entity.Id);
        Assert.Null(entity);
    }
}