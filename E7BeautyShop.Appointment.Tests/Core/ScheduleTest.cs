using E7BeautyShop.Appointment.Core;

namespace E7BeautyShop.Appointment.Tests.Core;

public class ScheduleTest
{
    private readonly TimeSpan _startWeekday = new(8, 0, 0);
    private readonly TimeSpan _endWeekday = new(18, 0, 0);
    private readonly TimeSpan _startWeekend = new(8, 0, 0);
    private readonly TimeSpan _endWeekend = new(12, 0, 0);

    [Fact]
    public void Should_AddDayRest_WhenCalled_ShouldAddDayRestToList()
    {
        var professionalId = new Professional(Guid.NewGuid());
        var weekday = new Weekday(_startWeekday, _endWeekday);
        var weekend = new Weekend(_startWeekend, _endWeekend);
        var schedule =
            new Schedule(DateTime.Now, DateTime.Now.AddDays(7), professionalId, weekday, weekend);
        var dayRest = new DayRest(DayOfWeek.Monday);

        var officeHour1 = new OfficeHour();
        officeHour1.CreateOfficeHour(DateTime.Now);
        var officeHour2 = new OfficeHour();
        officeHour2.CreateOfficeHour(DateTime.Now.AddDays(1));
        schedule.AddOfficeHour(officeHour1);
        schedule.AddOfficeHour(officeHour2);

        schedule.AddDayRest(dayRest);

        Assert.NotNull(schedule);
        Assert.Contains(dayRest, schedule.DaysRest);
        Assert.Single(schedule.DaysRest);
        Assert.Equal(DayOfWeek.Monday, schedule.DaysRest[0].DayOnWeek);
        Assert.Equal(professionalId, schedule.Professional);
        Assert.NotEmpty(schedule.OfficeHours);
        Assert.Equal(2, schedule.OfficeHours.Count);
    }

    [Fact]
    public void Should_ReturnsTrue_IfWeekday_WhenOfficeHourIsOnWeekday()
    {
        var officeHour = new OfficeHour();
        officeHour.CreateOfficeHour(new DateTime(2024, 5, 30, 10, 0, 0, DateTimeKind.Local));
        var schedule = new Schedule(DateTime.Now, DateTime.Now.AddDays(1),
            new Professional(Guid.NewGuid()),
            new Weekday(_startWeekday, _endWeekday), new Weekend(_startWeekend, _endWeekend));
        var isWeekday = Schedule.IsWeekday(officeHour);

        Assert.True(isWeekday);
    }

    [Fact]
    public void IsWeekday_ReturnsFalse_WhenOfficeHourIsOnWeekend()
    {
        var officeHour = new OfficeHour();
        officeHour.CreateOfficeHour(new DateTime(2024, 5, 26, 10, 0, 0, DateTimeKind.Local));
        var schedule = new Schedule(DateTime.Now, DateTime.Now.AddDays(1), new Professional(Guid.NewGuid()),
            new Weekday(_startWeekday, _endWeekday), new Weekend(_startWeekday, _endWeekday));
        var isWeekday = Schedule.IsWeekday(officeHour);

        Assert.False(isWeekday);
    }

    [Fact]
    public void Should_AddOfficeHour_WhenNotDayRest()
    {
        var dateTime = new DateTime(2024, 5, 2, 0, 0, 0, DateTimeKind.Local);
        var schedule = new Schedule(DateTime.Now, dateTime, new Professional(Guid.NewGuid()),
            new Weekday(_startWeekday, _endWeekday), new Weekend(_startWeekend, _endWeekend));
        var officeHour = new OfficeHour();
        officeHour.CreateOfficeHour(new DateTime(2024, 5, 31, 10, 0, 0, DateTimeKind.Local));
        schedule.AddDayRest(new DayRest(DayOfWeek.Friday));
        schedule.AddOfficeHour(officeHour);

        Assert.DoesNotContain(officeHour, schedule.OfficeHours);
    }
}