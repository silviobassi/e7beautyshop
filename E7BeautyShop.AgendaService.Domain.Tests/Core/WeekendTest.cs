using E7BeautyShop.AgendaService.Domain.ObjectsValue;
using E7BeautyShop.AgendaService.Domain.ValueObjects;

namespace E7BeautyShop.AgendaService.Domain.Tests.UnitTests.Core;

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

