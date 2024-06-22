using E7BeautyShop.Appointment.Application.Ports;
using E7BeautyShop.Appointment.Core.Entities;
using E7BeautyShop.Appointment.Core.ObjectsValue;
using Microsoft.Extensions.DependencyInjection;

namespace E7BeautyShop.Appointment.Tests.IntegrationTests;

public class SchedulePersistenceTests(TestStartup startup) : IClassFixture<TestStartup>
{
    private readonly ISchedulePersistencePort _schedulePersistence =
        startup.ServiceProvider.GetRequiredService<ISchedulePersistencePort>();

    [Fact]
    public async Task Should_Persistence_Schedule()
    {
        var startAt = DateTime.Now;
        var endAt = DateTime.Now.AddDays(4);

        var id = Guid.Parse("e2977a5c-f1d6-46fc-8f7e-f78d0dda568c");
        
        ProfessionalId professionalId = id;
        Weekday weekday = (TimeSpan.FromHours(8), TimeSpan.FromHours(18));
        Weekend weekend = (TimeSpan.FromHours(8), TimeSpan.FromHours(12));

        var schedule = Appointment.Core.Entities.Agenda.Create(startAt, endAt, professionalId, weekday, weekend);

        var dayRestSunday = DayRest.Create(DayOfWeek.Sunday);
        var dayRestMonday = DayRest.Create(DayOfWeek.Monday);
        
        schedule.AddDayRest(dayRestSunday);
        schedule.AddDayRest(dayRestMonday);
        
        await _schedulePersistence.CreateAsync(schedule);

        var currentSchedule = await _schedulePersistence.GetByIdAsync(schedule.Id);

        Assert.NotNull(currentSchedule);
        Assert.Equal(schedule.Id, currentSchedule.Id);
        Assert.Equal(schedule.StartAt, currentSchedule.StartAt);
        Assert.Equal(schedule.EndAt, currentSchedule.EndAt);
        Assert.Equal(schedule.ProfessionalId, currentSchedule.ProfessionalId);
        Assert.Equal(schedule.Weekday, currentSchedule.Weekday);
        Assert.Equal(schedule.Weekend, currentSchedule.Weekend);
        Assert.Equal(schedule.DaysRest.Count, currentSchedule.DaysRest.Count);
        
        var deleteAsync = await _schedulePersistence.DeleteAsync(schedule);
        var deletedSchedule = await _schedulePersistence.GetByIdAsync(deleteAsync!.Id);
        Assert.Null(deletedSchedule);
    }

    [Fact]
    public async Task Should_Update_Schedule()
    {
        var startAt = DateTime.Now;
        var endAt = DateTime.Now.AddDays(4);

        var id = Guid.Parse("e2977a5c-f1d6-46fc-8f7e-f78d0dda568c");
        ProfessionalId professionalId = id;
        Weekday weekday = (TimeSpan.FromHours(8), TimeSpan.FromHours(18));
        Weekend weekend = (TimeSpan.FromHours(8), TimeSpan.FromHours(12));

        var schedule = Appointment.Core.Entities.Agenda.Create(startAt, endAt, professionalId, weekday, weekend);

        var dayRestSunday = DayRest.Create(DayOfWeek.Sunday);
        var dayRestMonday = DayRest.Create(DayOfWeek.Monday);
        var officeHour1 = OfficeHour.Create(DateTime.Now, 30);
        var officeHour2 = OfficeHour.Create(DateTime.Now.AddMinutes(30), 30);
        
        schedule.AddDayRest(dayRestSunday);
        schedule.AddDayRest(dayRestMonday);
        schedule.AddOfficeHour(officeHour1);
        schedule.AddOfficeHour(officeHour2);
        
        await _schedulePersistence.CreateAsync(schedule);
        var currentSchedule = await _schedulePersistence.GetByIdAsync(schedule.Id);
        
        var newId = Guid.Parse("6905d076-d8ea-4017-adcf-8cb42c485d9c");
        var newStartAt = DateTime.Now.AddDays(5);
        var newEndAt = DateTime.Now.AddDays(9);
        Weekday newWeekday = (TimeSpan.FromHours(8), TimeSpan.FromHours(16));
        Weekend newWeekend = (TimeSpan.FromHours(8), TimeSpan.FromHours(14));
        var dayRestWednesday = DayRest.Create(DayOfWeek.Wednesday);
        var newOfficeHour = OfficeHour.Create(DateTime.Now.AddHours(1), 30);
        
        currentSchedule?.Update(schedule.Id, newStartAt, newEndAt, newId, newWeekday, newWeekend);
        currentSchedule?.RemoveDayRest(dayRestMonday);
        currentSchedule?.RemoveDayRest(dayRestSunday);
        currentSchedule?.RemoveOfficeHour(officeHour1);
        currentSchedule?.RemoveOfficeHour(officeHour2);
        currentSchedule?.AddOfficeHour(newOfficeHour);
        currentSchedule?.AddDayRest(dayRestWednesday);
        
        await _schedulePersistence.UpdateAsync(currentSchedule!);
        var updatedSchedule = await _schedulePersistence.GetByIdAsync(currentSchedule!.Id);
        
        Assert.Equal(currentSchedule.Id, updatedSchedule!.Id);
        Assert.Equal(currentSchedule.StartAt, updatedSchedule.StartAt);
        Assert.Equal(currentSchedule.EndAt, updatedSchedule.EndAt);
        Assert.Equal(currentSchedule.ProfessionalId, updatedSchedule.ProfessionalId);
        Assert.Equal(currentSchedule.Weekday, updatedSchedule.Weekday);
        Assert.Equal(currentSchedule.Weekend, updatedSchedule.Weekend);
        Assert.Equal(currentSchedule.DaysRest.Count, updatedSchedule.DaysRest.Count);
        Assert.Single(updatedSchedule.DaysRest);
        Assert.Single(updatedSchedule.OfficeHours);
        Assert.Contains(DayOfWeek.Wednesday, updatedSchedule.DaysRest.Select(dr => dr.DayOnWeek));
        Assert.Contains(newOfficeHour, updatedSchedule.OfficeHours);
        var deleteAsync = await _schedulePersistence.DeleteAsync(updatedSchedule);
        var deletedSchedule = await _schedulePersistence.GetByIdAsync(deleteAsync!.Id);
        Assert.Null(deletedSchedule);
    }
}