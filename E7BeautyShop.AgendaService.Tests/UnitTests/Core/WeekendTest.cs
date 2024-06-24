using E7BeautyShop.AgendaService.Core.ObjectsValue;

namespace E7BeautyShop.AgendaService.Tests.UnitTests.Core;

public class WeekendTest
{
    [Fact]
    private void Should_CreateWeekend()
    {
        var start = new TimeSpan(8,0,0);
        var end = new TimeSpan(12,0,0);
        Weekend weekend = (start, end);
        Assert.NotNull(weekend);
        Assert.Equal(start, weekend.StartAt);
        Assert.Equal(end, weekend.EndAt);
    }
}

