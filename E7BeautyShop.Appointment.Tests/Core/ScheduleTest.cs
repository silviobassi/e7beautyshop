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
        ProfessionalId professionalId = Guid.NewGuid();
        var weekday = (_startWeekday, _endWeekday);
        var weekend = (_startWeekend, _endWeekend);
        var schedule = Schedule.Create(DateTime.Now, DateTime.Now.AddDays(7), professionalId, weekday, weekend);
        var dayRest = DayRest.Create(DayOfWeek.Monday);
        
        var officeHour1 = OfficeHour.Create(DateTime.Now, 30);
        var officeHour2 = OfficeHour.Create(DateTime.Now.AddDays(1), 20);
        schedule.AddOfficeHour(officeHour1);
        schedule.AddOfficeHour(officeHour2);

        schedule.AddDayRest(dayRest);

        Assert.NotNull(schedule);
        Assert.Equal(professionalId, schedule.ProfessionalId);
        Assert.NotEmpty(schedule.OfficeHours);
        Assert.Equal(2, schedule.OfficeHours.Count);
    }

    [Fact]
    public void Should_ReturnsTrue_IfWeekday_WhenOfficeHourIsOnWeekday()
    {
        var officeHour = OfficeHour.Create(new DateTime(2024, 5, 30, 10, 0, 0, DateTimeKind.Local), 30);
        
        Weekday weekday = (_startWeekday, _endWeekday);
        Weekend weekend = (_startWeekend, _endWeekend);
        
        ProfessionalId professionalId = Guid.NewGuid();
        
        var schedule = Schedule.Create(DateTime.Now, DateTime.Now.AddDays(1), professionalId, weekday,weekend);
        var isWeekday = schedule.IsWeekday(officeHour);

        Assert.True(isWeekday);
    }

    [Fact]
    public void IsWeekday_ReturnsFalse_WhenOfficeHourIsOnWeekend()
    {
        var officeHour = OfficeHour.Create(new DateTime(2024, 5, 26, 10, 0, 0, DateTimeKind.Local), 30);
        
        Weekday weekday = (_startWeekday, _endWeekday);
        Weekend weekend = (_startWeekend, _endWeekend);
        
        ProfessionalId professionalId = Guid.NewGuid();
        
        var schedule = Schedule.Create(DateTime.Now, DateTime.Now.AddDays(1), professionalId, weekday, weekend);
        var isWeekday = schedule.IsWeekday(officeHour);

        Assert.False(isWeekday);
    }

    [Fact]
    public void Should_AddOfficeHour_WhenNotDayRest()
    {
        var dateTime = new DateTime(2024, 5, 2, 0, 0, 0, DateTimeKind.Local);
        
        Weekday weekday = (_startWeekday, _endWeekday);
        Weekend weekend = (_startWeekend, _endWeekend);
        
        ProfessionalId professionalId = Guid.NewGuid();
        
        var schedule = Schedule.Create(DateTime.Now, dateTime, professionalId, weekday, weekend);
        var officeHour = OfficeHour.Create(new DateTime(2024, 5, 31, 10, 0, 0, DateTimeKind.Local), 30);
        schedule.AddDayRest(DayRest.Create(DayOfWeek.Friday));
        schedule.AddOfficeHour(officeHour);

        Assert.DoesNotContain(officeHour, schedule.OfficeHours);
    }
}