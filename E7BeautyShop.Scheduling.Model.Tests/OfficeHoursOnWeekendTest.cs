namespace E7BeautyShop.Domain.Tests;

public class OfficeHoursOnWeekendTest
{
    [Fact]
    public void Should_Create_WeekendOfficeHours()
    {
        var startAt = new TimeSpan(8, 0, 0);
        var endAt = new TimeSpan(12, 0, 0);
        OfficeHoursOnWeekend officeHoursOnWeekend = new(startAt, endAt);
        Assert.Equal(startAt, officeHoursOnWeekend.StartAt);
        Assert.Equal(endAt, officeHoursOnWeekend.EndAt);
    }
}