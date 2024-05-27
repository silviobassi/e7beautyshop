using Xunit.Abstractions;

namespace E7BeautyShop.Schedule.Tests;

public class OfficeDayTest(ITestOutputHelper output)
{
    [Fact]
    public void Should_CreateOfficeDay()
    {
        var dateTime = new DateTime(2021, 1, 1, 11, 1, 1, DateTimeKind.Local);
        var officeDay = new OfficeDay(dateTime);
        Assert.NotNull(officeDay);
        Assert.Equal(dateTime, officeDay.DateTime);
        Assert.Empty(officeDay.TimesOfDay);

        for (var i = dateTime; i < dateTime.AddDays(1); i = i.AddMinutes(30))
        {
            officeDay.AddOfficeHour(new OfficeHour(i.TimeOfDay));
        }

        Assert.NotEmpty(officeDay.TimesOfDay);
        Assert.Equal(48, officeDay.TimesOfDay.Count);
    }

    [Fact]
    public void Attend_WhenIsAttendIsTrue_ShouldReturnTrue()
    {
        Appointment officeDay = new OfficeDay(DateTime.Now);
        officeDay.Attend();
        Assert.True(officeDay.IsAvailable);
    }

    [Fact]
    public void NotAttend_WhenIsAttendIsTrue_ShouldSetIsAttendToFalse()
    {
        var officeDay = new OfficeDay(DateTime.Now);
        officeDay.Cancel();
        Assert.False(officeDay.IsAvailable);
    }

    [Fact]
    public void NotAttend_WhenIsAttendIsFalse_ShouldSetIsAttendToTrue()
    {
        var officeDay = new OfficeDay(DateTime.Now);
        officeDay.Cancel();
        officeDay.Attend();
        Assert.True(officeDay.IsAvailable);
    }
}