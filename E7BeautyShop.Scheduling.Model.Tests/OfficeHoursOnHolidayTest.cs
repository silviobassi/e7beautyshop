namespace E7BeautyShop.Domain.Tests;

public class OfficeHoursOnHolidayTest
{
    [Fact]
    public void Should_Create_HolidayOfficeHours()
    {
        var startAt = new TimeSpan(8, 0, 0);
        var endAt = new TimeSpan(16, 0, 0);
        OfficeHoursOnHoliday officeHoursOnHoliday = new(startAt, endAt);
        Assert.Equal(startAt, officeHoursOnHoliday.StartAt);
        Assert.Equal(endAt, officeHoursOnHoliday.EndAt);
    }
}