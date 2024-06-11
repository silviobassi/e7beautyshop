using E7BeautyShop.Appointment.Application.Ports;
using E7BeautyShop.Appointment.Core.Entities;
using E7BeautyShop.Appointment.Core.ObjectsValue;
using Microsoft.Extensions.DependencyInjection;

namespace E7BeautyShop.Appointment.Tests.UnitTests.Adapters.Outbound.Persistence;

public class SchedulePersistenceTest
{
    private readonly ISchedulePersistencePort _schedulePersistence;

    public SchedulePersistenceTest()
    {
        var startup = new TestStartup();
        _schedulePersistence = startup.ServiceProvider.GetRequiredService<ISchedulePersistencePort>();
    }

    [Fact]
    public void Should_Create_Schedule()
    {
        var startAt = DateTime.Now;
        var endAt = DateTime.Now.AddDays(4);

        var id = Guid.NewGuid();
        
        ProfessionalId professionalId = id;
        Weekday weekday = (TimeSpan.FromHours(8), TimeSpan.FromHours(18));
        Weekend weekend = (TimeSpan.FromHours(8), TimeSpan.FromHours(12));
        
        var schedule = Schedule.Create(startAt, endAt, professionalId, weekday, weekend);
        var entity = _schedulePersistence.Create(schedule);
        
        Assert.NotNull(entity);
        Assert.Equal(schedule.Id, entity.Id);
        Assert.Equal(schedule.StartAt, entity.StartAt);
        Assert.Equal(schedule.EndAt, entity.EndAt);
        Assert.Equal(schedule.ProfessionalId, entity.ProfessionalId);
        Assert.Equal(schedule.Weekday, entity.Weekday);
        Assert.Equal(schedule.Weekend, entity.Weekend);
    }
    
    [Fact]
    public void Should_Update_Schedule()
    {
        var startAt = DateTime.Now;
        var endAt = DateTime.Now.AddDays(4);

        var id = Guid.NewGuid();
        
        ProfessionalId professionalId = id;
        Weekday weekday = (TimeSpan.FromHours(8), TimeSpan.FromHours(18));
        Weekend weekend = (TimeSpan.FromHours(8), TimeSpan.FromHours(12));
        
        var schedule = Schedule.Create(startAt, endAt, professionalId, weekday, weekend);
        var entity = _schedulePersistence.Update(schedule);
        
        Assert.NotNull(entity);
        Assert.Equal(schedule.Id, entity.Id);
        Assert.Equal(schedule.StartAt, entity.StartAt);
        Assert.Equal(schedule.EndAt, entity.EndAt);
        Assert.Equal(schedule.ProfessionalId, entity.ProfessionalId);
        Assert.Equal(schedule.Weekday, entity.Weekday);
        Assert.Equal(schedule.Weekend, entity.Weekend);
    }
    
    [Fact]
    public void Should_Delete_Schedule()
    {
        var startAt = DateTime.Now;
        var endAt = DateTime.Now.AddDays(4);

        var id = Guid.NewGuid();
        
        ProfessionalId professionalId = id;
        Weekday weekday = (TimeSpan.FromHours(8), TimeSpan.FromHours(18));
        Weekend weekend = (TimeSpan.FromHours(8), TimeSpan.FromHours(12));
        
        var schedule = Schedule.Create(startAt, endAt, professionalId, weekday, weekend);
        var entity = _schedulePersistence.Delete(schedule);
        Assert.NotNull(entity);
        Assert.Equal(schedule.Id, entity.Id);
        Assert.Equal(schedule.StartAt, entity.StartAt);
        Assert.Equal(schedule.EndAt, entity.EndAt);
        Assert.Equal(schedule.ProfessionalId, entity.ProfessionalId);
        Assert.Equal(schedule.Weekday, entity.Weekday);
        Assert.Equal(schedule.Weekend, entity.Weekend);
    }
    
    [Fact]
    public async Task Should_NotGet_Schedule()
    {
        var startAt = DateTime.Now;
        var endAt = DateTime.Now.AddDays(4);

        var id = Guid.NewGuid();
        
        ProfessionalId professionalId = id;
        Weekday weekday = (TimeSpan.FromHours(8), TimeSpan.FromHours(18));
        Weekend weekend = (TimeSpan.FromHours(8), TimeSpan.FromHours(12));
        
        var schedule = Schedule.Create(startAt, endAt, professionalId, weekday, weekend);
        _schedulePersistence.Create(schedule);
        var entity = await _schedulePersistence.Get(x => x.Id == schedule.Id);
        Assert.Null(entity);
    }
}