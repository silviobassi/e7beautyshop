using Xunit.Abstractions;

namespace E7BeautyShop.Domain.Tests;

public class ScheduleTest(ITestOutputHelper output)
{
    [Fact]
    public void Should_Create_Schedule()
    {
        var startAt = new DateTime(2024, 05, 01);
        var endAt = new DateTime(2024, 07, 31);
        Schedule schedule = new(startAt, endAt);
        Assert.NotNull(schedule);
    }

    [Fact]
    public void Should_Generate_Schedule()
    {
        var startAt = new DateTime(2024, 05, 01);
        var endAt = new DateTime(2024, 07, 31);

        var officeDay = new OfficeDay(
            30, 
            new Weekday(new TimeSpan(8, 0, 0), new TimeSpan(18, 0, 0)), 
            new Weekend(new TimeSpan(9, 0, 0), new TimeSpan(13, 0, 0)), 
            DayOfWeek.Monday);

        Schedule schedule = new(startAt, endAt);
        Assert.NotNull(schedule);
        schedule.Generate(officeDay);
        
        Assert.Equal(92, schedule.OfficeDays.Count);
        
        foreach (var ofDay in schedule.OfficeDays)
        {
            output.WriteLine($"Date: {ofDay?.Date}");
            
            foreach (var officeHour in ofDay!.OfficeHours)
            {
                output.WriteLine($"Hour: {officeHour.Hour}");
            }
        }
    }
}