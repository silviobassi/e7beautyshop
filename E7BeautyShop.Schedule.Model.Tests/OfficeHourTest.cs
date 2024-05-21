namespace E7BeautyShop.Domain.Tests;

public class OfficeHourTest
{
    [Fact]
    public void Should_Initialize_As_Available()
    {
        var officeHour = OfficeHour;
        Assert.True(officeHour.IsAvailable);
    }

    [Fact]
    public void Should_Become_Unavailable_When_Made_Unavailable()
    {
        var officeHour = OfficeHour;
        officeHour.ToMakeUnavailable();
        Assert.False(officeHour.IsAvailable);
    }

    [Fact]
    public void Should_Become_Available_When_Made_Available()
    {
        var officeHour = OfficeHour;
        officeHour.ToMakeUnavailable();
        officeHour.ToMakeAvailable();
        Assert.True(officeHour.IsAvailable);
    }

    [Fact]
    public void Should_Keep_Hour_When_Made_Unavailable()
    {
        var officeHour = OfficeHour;
        officeHour.ToMakeUnavailable();
        Assert.Equal(OfficeHour.Hour, officeHour.Hour);
    }

    [Fact]
    public void Should_Keep_Hour_When_Made_Available()
    {
        var officeHour = OfficeHour;
        officeHour.ToMakeAvailable();
        Assert.Equal(OfficeHour.Hour, officeHour.Hour);
    }

    private static OfficeHour OfficeHour
    {
        get
        {
            var officeHour = new OfficeHour();
            return officeHour;
        }
    }
}