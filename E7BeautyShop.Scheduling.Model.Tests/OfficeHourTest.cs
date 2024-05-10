namespace E7BeautyShop.Domain.Tests;

public class OfficeHourTest
{
    [Fact]
    public void Should_Initialize_As_Available()
    {
        var officeHour = new OfficeHour(new TimeSpan(9, 0, 0));
        Assert.True(officeHour.IsAvailable);
    }

    [Fact]
    public void Should_Become_Unavailable_When_Made_Unavailable()
    {
        var officeHour = new OfficeHour(new TimeSpan(9, 0, 0));
        officeHour.ToMakeUnavailable();
        Assert.False(officeHour.IsAvailable);
    }

    [Fact]
    public void Should_Become_Available_When_Made_Available()
    {
        var officeHour = new OfficeHour(new TimeSpan(9, 0, 0));
        officeHour.ToMakeUnavailable();
        officeHour.ToMakeAvailable();
        Assert.True(officeHour.IsAvailable);
    }

    [Fact]
    public void Should_Keep_Hour_When_Made_Unavailable()
    {
        var officeHour = new OfficeHour(new TimeSpan(9, 0, 0));
        officeHour.ToMakeUnavailable();
        Assert.Equal(new TimeSpan(9, 0, 0), officeHour.Hour);
    }

    [Fact]
    public void Should_Keep_Hour_When_Made_Available()
    {
        var officeHour = new OfficeHour(new TimeSpan(9, 0, 0));
        officeHour.ToMakeAvailable();
        Assert.Equal(new TimeSpan(9, 0, 0), officeHour.Hour);
    }
}