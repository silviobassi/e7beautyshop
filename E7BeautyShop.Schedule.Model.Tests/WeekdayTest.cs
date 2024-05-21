namespace E7BeautyShop.Schedule.Tests;

public class WeekdayTest
{
    [Fact]
    public void Should_Create_Weekday()
    {
        var startAt = new TimeSpan(8,0,0);
        var endAt = new TimeSpan(16,0,0);
        Weekday weekday = new (startAt, endAt);
        Assert.NotNull(weekday);
        Assert.Equal(startAt, weekday.StartAt);
        Assert.Equal(endAt, weekday.EndAt);
    }
}