using Xunit.Abstractions;

namespace E7BeautyShop.Schedule.Tests;

public class ScheduleTest(ITestOutputHelper output)
{
    private readonly TimeSpan _startWeekday = new(8, 0, 0);
    private readonly TimeSpan _endWeekday = new(18, 0, 0);
    private readonly TimeSpan _startWeekend = new(8, 0, 0);
    private readonly TimeSpan _endWeekend = new(12, 0, 0);
    
    [Fact]
    public void AddDayRest_WhenCalled_ShouldAddDayRestToList()
    {
        var professionalId = new ProfessionalId(Guid.NewGuid());
        var weekday = new Weekday(_startWeekday, _endWeekday);
        var weekend = new Weekend(_startWeekend, _endWeekend);
        var schedule = new Schedule(DateTime.Now, DateTime.Now.AddDays(7), professionalId, weekday, weekend);
        var dayRest = new DayRest(DayOfWeek.Monday);
        schedule.AddDayRest(dayRest);
        Assert.Contains(dayRest, schedule.DaysRest);
    }
}