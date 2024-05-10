using Microsoft.VisualBasic;
using Xunit.Abstractions;

namespace E7BeautyShop.Domain.Tests;

public class OfficeDayTest(ITestOutputHelper output)
{
    private readonly ITestOutputHelper _output = output;

    [Fact]
    public void Should_Create_OfficeDay()
    {
        const int interval = 30;
        var startWeekDay = new TimeSpan(8, 0, 0);
        var endWeekDay = new TimeSpan(18, 0, 0);
        var startWeekend = new TimeSpan(8, 0, 0);
        var endWeekend = new TimeSpan(12, 0, 0);

        OfficeDay officeDay = new(
            new DateTime(2024, 5, 2),
            interval,
            startWeekDay,
            endWeekDay,
            startWeekend,
            endWeekend);
        Assert.NotNull(officeDay);
        Assert.Equal(new DateTime(2024, 5, 2), officeDay.Date);
        Assert.Equal(30, officeDay.Interval);
    }

    [Fact]
    public void Should_Generate_OfficeHours_Weekday()
    {
        const int interval = 30;
        var startWeekDay = new TimeSpan(8, 0, 0);
        var endWeekDay = new TimeSpan(18, 0, 0);
        var startWeekend = new TimeSpan(8, 0, 0);
        var endWeekend = new TimeSpan(12, 0, 0);
        OfficeDay officeDay = new(
            new DateTime(2024, 5, 2),
            interval,
            startWeekDay,
            endWeekDay,
            startWeekend,
            endWeekend);
        officeDay.GenerateWeekday();
        Assert.Equal(21, officeDay.OfficeHours.Count);
        Assert.Equal(startWeekDay, officeDay.OfficeHours[0].Hour);
        Assert.Equal(endWeekDay, officeDay.OfficeHours[20].Hour);

        foreach (var officeHour in officeDay.OfficeHours)
        {
            _output.WriteLine(officeHour.Hour.ToString());
        }
    }

    [Fact]
    public void Should_Generate_Weekend()
    {
        const int interval = 30;
        var date = new DateTime(2024, 5, 11);
        var startWeekDay = new TimeSpan(8, 0, 0);
        var endWeekDay = new TimeSpan(18, 0, 0);
        var startWeekend = new TimeSpan(8, 0, 0);
        var endWeekend = new TimeSpan(12, 0, 0);
        
        OfficeDay officeDay = new(
            date,
            interval,
            startWeekDay,
            endWeekDay,
            startWeekend,
            endWeekend);
        
        officeDay.GenerateWeekend();
        Assert.Equal(9, officeDay.OfficeHours.Count);
        Assert.Equal(startWeekend, officeDay.OfficeHours[0].Hour);
        Assert.Equal(endWeekend, officeDay.OfficeHours[8].Hour);

        foreach (var officeHour in officeDay.OfficeHours)
        {
            _output.WriteLine(officeHour.Hour.ToString());
        }
    }
}