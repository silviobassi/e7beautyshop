using E7BeautyShop.Appointment.Core;

namespace E7BeautyShop.Appointment.Tests.Core;

public class WeekendTest
{
    [Fact]
    private void Should_CreateWeekend()
    {
        var start = new TimeSpan(8,0,0);
        var end = new TimeSpan(12,0,0);
        var weekend = new Weekend(start, end);
        Assert.NotNull(weekend);
        Assert.Equal(start, weekend.StartAt);
        Assert.Equal(end, weekend.EndAt);
    }
}

