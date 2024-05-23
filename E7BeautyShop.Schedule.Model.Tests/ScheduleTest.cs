using Xunit.Abstractions;

namespace E7BeautyShop.Schedule.Tests;

public class ScheduleTest(ITestOutputHelper output)
{
    private readonly DateTime _startAt = new(2024, 5, 22, 0, 0, 0, DateTimeKind.Local);
    private readonly DateTime _endAt = new(2024, 5, 26, 0, 0, 0, DateTimeKind.Local);
    private readonly TimeSpan _startWeekday = new(8, 0, 0);
    private readonly TimeSpan _endWeekday = new(18, 0, 0);
    private readonly TimeSpan _startWeekend = new(8, 0, 0);
    private readonly TimeSpan _endWeekend = new(12, 0, 0);
    
    [Fact]
    public void Should_CreateSchedule()
    {
        var weekday = new Weekday(_startWeekday, _endWeekday);
        var weekend = new Weekend(_startWeekend, _endWeekend);
        var dayRest = new List<DayRest> { new (DayOfWeek.Thursday), new(DayOfWeek.Friday), new (DayOfWeek.Sunday) };
        
        var schedule = new Schedule(_startAt, _endAt, weekday, weekend);
        schedule.DaysRest.AddRange(dayRest);

        Assert.NotNull(schedule);
        Assert.Equal(_startAt, schedule.StartAt);
        Assert.Equal(_endAt, schedule.EndAt);
        Assert.NotEmpty(schedule.DaysRest);
        
        DaysAndTimesIntoTheSchedule.Build(schedule, 30);
        
        Assert.NotEmpty(schedule.OfficeDays);
        Assert.Equal(2, schedule.OfficeDays.Count);
        Assert.Equal(30, schedule.OfficeDays.SelectMany(day => day.TimesOfDay).Count());
    }
}