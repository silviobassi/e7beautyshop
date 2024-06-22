using E7BeautyShop.Appointment.Core.Entities;
using E7BeautyShop.Appointment.Core.Validations;
using Xunit.Abstractions;

namespace E7BeautyShop.Appointment.Tests.UnitTests.Core;

public class ScheduleTest(ITestOutputHelper output)
{
    private readonly TimeSpan _startWeekday = new(8, 0, 0);
    private readonly TimeSpan _endWeekday = new(18, 0, 0);
    private readonly TimeSpan _startWeekend = new(8, 0, 0);
    private readonly TimeSpan _endWeekend = new(12, 0, 0);

    [Fact]
    public void Should_CreateSchedule_WithValidParameters()
    {
        var startAt = DateTime.Now;
        var endAt = DateTime.Now.AddDays(7);
        var professionalId = Guid.NewGuid();
        var weekday = (_startWeekday, _endWeekday);
        var weekend = (_startWeekend, _endWeekend);

        var schedule = Schedule.Create(startAt, endAt, professionalId, weekday, weekend);

        Assert.NotNull(schedule);
        Assert.Equal(startAt, schedule.StartAt);
        Assert.Equal(endAt, schedule.EndAt);
        Assert.Equal(professionalId, schedule.ProfessionalId);
        Assert.Equal(weekday, schedule.Weekday);
        Assert.Equal(weekend, schedule.Weekend);
    }

    [Fact]
    public void Should_ThrowException_WhenCreatingScheduleWithInvalidParameters()
    {
        var startAt = DateTime.MinValue;
        var endAt = DateTime.MinValue;
        var professionalId = Guid.NewGuid();
        var weekday = (_startWeekday, _endWeekday);
        var weekend = (_startWeekend, _endWeekend);

        Assert.Throws<BusinessException>(() => Schedule.Create(startAt, endAt, professionalId, weekday, weekend));
    }

    [Fact]
    public void Should_AddOfficeHour_WhenNotDayRest()
    {
        var startAt = new DateTime(2024, 06, 18, 8, 0, 0, DateTimeKind.Utc);
        var endAt = startAt.AddDays(7);
        var professionalId = Guid.NewGuid();
        var weekday = (_startWeekday, _endWeekday);
        var weekend = (_startWeekend, _endWeekend);
        var schedule = Schedule.Create(startAt, endAt, professionalId, weekday, weekend);
        var officeHour = OfficeHour.Create(startAt, 30);

        schedule.AddOfficeHour(officeHour);

        Assert.Contains(officeHour, schedule.OfficeHours);
    }

    [Fact]
    public void Should_IsNotAvailable_WhenDayRest()
    {
        var startAt = new DateTime(2024, 06, 18, 8, 0, 0, DateTimeKind.Utc);
        var endAt = startAt.AddDays(7);
        var professionalId = Guid.NewGuid();
        var weekday = (_startWeekday, _endWeekday);
        var weekend = (_startWeekend, _endWeekend);
        var schedule = Schedule.Create(startAt, endAt, professionalId, weekday, weekend);
        var officeHour = OfficeHour.Create(startAt, 30);
        var dayRest = DayRest.Create(startAt.DayOfWeek);

        schedule.AddDayRest(dayRest);
        schedule.AddOfficeHour(officeHour);

        Assert.False(officeHour.IsAvailable);
    }

    [Fact]
    public void Should_UpdateSchedule_WithValidParameters()
    {
        var startAt = new DateTime(2024, 06, 18, 8, 0, 0, DateTimeKind.Utc);
        var endAt = startAt.AddDays(7);
        var professionalId = Guid.NewGuid();
        var weekday = (_startWeekday, _endWeekday);
        var weekend = (_startWeekend, _endWeekend);
        var schedule = Schedule.Create(startAt, endAt, professionalId, weekday, weekend);

        var newStartAt = startAt.AddDays(1);
        var newEndAt = startAt.AddDays(8);
        var newProfessionalId = Guid.NewGuid();
        var newWeekday = (_startWeekday, _endWeekday);
        var newWeekend = (_startWeekend, _endWeekend);

        schedule.Update(schedule.Id, newStartAt, newEndAt, newProfessionalId, newWeekday, newWeekend);

        Assert.Equal(newStartAt, schedule.StartAt);
        Assert.Equal(newEndAt, schedule.EndAt);
        Assert.Equal(newProfessionalId, schedule.ProfessionalId);
        Assert.Equal(newWeekday, schedule.Weekday);
        Assert.Equal(newWeekend, schedule.Weekend);
    }
    
    [Fact]
    public void Should_ReturnTrue_WhenOfficeHourIsOnWeekday()
    {
        var officeHour = OfficeHour.Create(
            new DateTime(2024, 06, 18, 8, 0, 0, DateTimeKind.Utc), 30);
        var schedule = new Schedule();
        var result = schedule.IsWeekday(officeHour);
        Assert.True(result);
    }

    [Fact]
    public void Should_ReturnFalse_WhenOfficeHourIsOnWeekend()
    {
        var officeHour = OfficeHour.Create(
            new DateTime(2024, 06, 22, 8, 0, 0, DateTimeKind.Utc), 30);
        var schedule = new Schedule();
        var result = schedule.IsWeekday(officeHour);
        Assert.False(result);
    }
}