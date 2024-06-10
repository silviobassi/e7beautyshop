using E7BeautyShop.Appointment.Core;

namespace E7BeautyShop.Appointment.Tests.Core;

public class WeekdayTest
{
    [Fact]
    private void Should_CreateWeekday()
    {
        var start = new TimeSpan(8,0,0);
        var end = new TimeSpan(18, 0,0);
        var weekday = new Weekday(start, end);
        Assert.NotNull(weekday);
        Assert.Equal(start, weekday.StartAt);
        Assert.Equal(end, weekday.EndAt);
    }
}

