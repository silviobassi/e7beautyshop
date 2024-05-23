namespace E7BeautyShop.Schedule.Tests;

public class OfficeHourTest
{
    [Fact]
    void Should_CreateOfficeHour()
    {
        var timeOfDay = new TimeSpan(8,0,0);
        var officeHour = new OfficeHour(timeOfDay);
        Assert.NotNull(officeHour);
        Assert.Equal(timeOfDay, officeHour.TimeOfDay);
    }
}

