using Xunit.Abstractions;

namespace E7BeautyShop.Schedule.Tests;

public class OfficeDayTest(ITestOutputHelper output)
{
    [Fact]
    public void Should_CreateOfficeDay()
    {
        var dateTime = new DateTime(2021, 1, 1, 11, 0, 0, DateTimeKind.Local);
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
}