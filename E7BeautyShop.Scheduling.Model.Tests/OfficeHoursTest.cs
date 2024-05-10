namespace E7BeautyShop.Domain.Tests;

public class OfficeHoursTest
{
    [Fact]
    public void Should_Create_WeekdayOfficeHours()
    {
        var startAt = new TimeSpan(8, 0, 0);
        var endAt = new TimeSpan(16, 0, 0);
        OfficeHours officeHours = new(startAt, endAt);
        Assert.Equal(startAt, officeHours.StartAt);
        Assert.Equal(endAt, officeHours.EndAt);
    }
}

