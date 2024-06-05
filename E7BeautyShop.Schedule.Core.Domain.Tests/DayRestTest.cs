namespace E7BeautyShop.Schedule.Core.Domain.Tests;

public class DayRestTest
{
    [Fact]
    private void Should_CreateDayRest()
    {
        const DayOfWeek dayOnWeek = DayOfWeek.Sunday;
        var dayRest = new DayRest(dayOnWeek);
        Assert.NotNull(dayRest);
        Assert.Equal(dayOnWeek, dayRest.DayOnWeek);
    }
}

