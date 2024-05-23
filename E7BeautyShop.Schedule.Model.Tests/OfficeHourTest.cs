namespace E7BeautyShop.Schedule.Tests;

public class OfficeHourTest
{
    [Fact]
    private void Should_CreateOfficeHour()
    {
        var timeOfDay = new TimeSpan(8,0,0);
        var officeHour = new OfficeHour(timeOfDay);
        Assert.NotNull(officeHour);
        Assert.Equal(timeOfDay, officeHour.TimeOfDay);
    }

    [Fact]
    void Should_ThrowException_When_TimeOfDayIsInvalid()
    {
        Exception exception = Assert.Throws<BusinessException>(() => 
            new OfficeHour(new TimeSpan(0,0,0)));
        Assert.Equal("TimeOfDay cannot be empty", exception.Message);
    }

    [Fact]
    void Should_Cancel_OfficeHour()
    {
        var officeHour = new OfficeHour(new TimeSpan(8,0,0));
        officeHour.Cancel();
        Assert.False(officeHour.IsAvailable);
    }

    [Fact]
    void Should_BeAvailable_AfterCreation()
    {
        var officeHour = new OfficeHour(new TimeSpan(8,0,0));
        officeHour.Cancel();
        Assert.False(officeHour.IsAvailable);
        officeHour.Attend();
        Assert.True(officeHour.IsAvailable);
    }
}

