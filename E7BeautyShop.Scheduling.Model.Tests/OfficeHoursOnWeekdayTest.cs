namespace E7BeautyShop.Domain.Tests;

public class OfficeHoursOnWeekdayTest
{
    [Fact]
    public void Should_Create_WeekdayOfficeHours()
    {
        var startAt = new TimeSpan(8, 0, 0);
        var endAt = new TimeSpan(16, 0, 0);
        OfficeHoursOnWeekday officeHoursOnWeekday = new(startAt, endAt);
        Assert.Equal(startAt, officeHoursOnWeekday.StartAt);
        Assert.Equal(endAt, officeHoursOnWeekday.EndAt);
    }
}

