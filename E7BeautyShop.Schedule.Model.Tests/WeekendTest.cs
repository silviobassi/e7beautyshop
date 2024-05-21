namespace E7BeautyShop.Domain.Tests;

public class WeekendTest
{
    [Fact]
    public void Should_Create_Weekend()
    {
        var startAt = new TimeSpan(8,0,0);
        var endAt = new TimeSpan(16,0,0);
        Weekend weekend = new (startAt, endAt);
        Assert.NotNull(weekend);
        Assert.Equal(startAt, weekend.StartAt);
        Assert.Equal(endAt, weekend.EndAt);
    }
}