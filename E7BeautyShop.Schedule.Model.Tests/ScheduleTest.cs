namespace E7BeautyShop.Schedule.Tests;

public class ScheduleTest
{
    [Fact]
    public void ShouldCreateSchedule()
    {
        var startAt = DateTime.Now;
        const DayOfWeek dayRest = DayOfWeek.Monday;
        var weekday = new Weekday(new TimeSpan(8, 0, 0), new TimeSpan(18, 0, 0));
        var weekend = new Weekend(new TimeSpan(8, 0, 0), new TimeSpan(12, 0, 0));
        Schedule schedule = new(startAt, dayRest, weekday, weekend);
        var officeDay = new OfficeDay(startAt, weekday, weekend, dayRest);
        schedule.AddOfficeDay(officeDay);
        Assert.NotNull(schedule);
        Assert.Equal(startAt, schedule.StartAt);
        Assert.Equal(dayRest, schedule.DayRest);
        Assert.Equal(weekday, schedule.Weekday);
        Assert.Equal(weekend, schedule.Weekend);
        Assert.Contains(officeDay, schedule.OfficeDays);
    }
    
    [Fact]
    public void IsNotWeekday_ReturnsTrue_WhenStartAtIsSaturday()
    {
        var schedule = new Schedule(new DateTime(2024, 5, 25), 
            DayOfWeek.Monday, new Weekday(new TimeSpan(8, 0, 0), 
                new TimeSpan(18, 0, 0)), new Weekend(new TimeSpan(8, 0, 0), 
                new TimeSpan(12, 0, 0)));
        Assert.True(schedule.IsNotWeekday);
    }

    [Fact]
    public void IsNotWeekday_ReturnsTrue_WhenStartAtIsSunday()
    {
        var schedule = new Schedule(new DateTime(2024, 5, 26), 
            DayOfWeek.Monday, new Weekday(new TimeSpan(8, 0, 0), 
                new TimeSpan(18, 0, 0)), new Weekend(new TimeSpan(8, 0, 0), 
                new TimeSpan(12, 0, 0)));
        Assert.True(schedule.IsNotWeekday);
    }

    [Fact]
    public void IsNotWeekday_ReturnsFalse_WhenStartAtIsWeekday()
    {
        var schedule = new Schedule(new DateTime(2024, 5, 24), 
            DayOfWeek.Monday, new Weekday(new TimeSpan(8, 0, 0), 
                new TimeSpan(18, 0, 0)), new Weekend(new TimeSpan(8, 0, 0), 
                new TimeSpan(12, 0, 0)));
        Assert.False(schedule.IsNotWeekday);
    }
}
