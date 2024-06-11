using E7BeautyShop.Appointment.Core;
using E7BeautyShop.Appointment.Core.ObjectsValue;

namespace E7BeautyShop.Appointment.Tests.Core;

public class WeekdayTest
{
    [Fact]
    private void Should_CreateWeekday()
    {
        var start = new TimeSpan(8,0,0);
        var end = new TimeSpan(18, 0,0);
        Weekday weekday = (start, end);
        Assert.NotNull(weekday);
        Assert.Equal(start, weekday.StartAt);
        Assert.Equal(end, weekday.EndAt);
    }
}

