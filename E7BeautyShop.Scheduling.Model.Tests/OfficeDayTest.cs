using Microsoft.VisualBasic;
using Xunit.Abstractions;

namespace E7BeautyShop.Domain.Tests;

public class OfficeDayTest(ITestOutputHelper output)
{
    [Fact]
    public void Should_Create_OfficeDay()
    {
        const int interval = 30;
        var startWeekDay = new TimeSpan(8, 0, 0);
        var endWeekDay = new TimeSpan(18, 0, 0);
        var startWeekend = new TimeSpan(8, 0, 0);
        var endWeekend = new TimeSpan(12, 0, 0);
        const DayOfWeek dayRest = DayOfWeek.Sunday;

        OfficeDay officeDay = new(
            new DateTime(2024, 5, 2),
            interval,
            startWeekDay,
            endWeekDay,
            startWeekend,
            endWeekend,
            dayRest);
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
        const DayOfWeek dayRest = DayOfWeek.Sunday;
        OfficeDay officeDay = new(
            new DateTime(2024, 5, 2),
            interval,
            startWeekDay,
            endWeekDay,
            startWeekend,
            endWeekend, 
            dayRest);
        officeDay.GenerateWeekday();
        Assert.Equal(21, officeDay.OfficeHours.Count);
        Assert.Equal(startWeekDay, officeDay.OfficeHours[0].Hour);
        Assert.Equal(endWeekDay, officeDay.OfficeHours[20].Hour);

        foreach (var officeHour in officeDay.OfficeHours)
        {
            output.WriteLine(officeHour.Hour.ToString());
        }
    }
    
    [Fact]
    public void Should_EmptyReturn_To_OfficeHours_Weekday_When_DayRest_Equal_DateDay()
    {
        const int interval = 30;
        var startWeekDay = new TimeSpan(8, 0, 0);
        var endWeekDay = new TimeSpan(18, 0, 0);
        var startWeekend = new TimeSpan(8, 0, 0);
        var endWeekend = new TimeSpan(12, 0, 0);
        const DayOfWeek dayRest = DayOfWeek.Monday;
        OfficeDay officeDay = new(
            new DateTime(2024, 5, 6),
            interval,
            startWeekDay,
            endWeekDay,
            startWeekend,
            endWeekend, 
            dayRest);
        officeDay.GenerateWeekday();
        Assert.Empty(officeDay.OfficeHours);
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
        const DayOfWeek dayRest = DayOfWeek.Sunday;
        OfficeDay officeDay = new(
            date,
            interval,
            startWeekDay,
            endWeekDay,
            startWeekend,
            endWeekend,
            dayRest);

        officeDay.GenerateWeekend();
        Assert.Equal(9, officeDay.OfficeHours.Count);
        Assert.Equal(startWeekend, officeDay.OfficeHours[0].Hour);
        Assert.Equal(endWeekend, officeDay.OfficeHours[8].Hour);

        foreach (var officeHour in officeDay.OfficeHours)
        {
            output.WriteLine(officeHour.Hour.ToString());
        }
    }
    
    [Fact]
    public void Should_EmptyReturn_To_OfficeHours_Weekend_When_DayRest_Equal_DateDay()
    {
        const int interval = 30;
        var startWeekDay = new TimeSpan(8, 0, 0);
        var endWeekDay = new TimeSpan(18, 0, 0);
        var startWeekend = new TimeSpan(8, 0, 0);
        var endWeekend = new TimeSpan(12, 0, 0);
        const DayOfWeek dayRest = DayOfWeek.Monday;
        
        OfficeDay officeDay = new(
            new DateTime(2024, 5, 6),
            interval,
            startWeekDay,
            endWeekDay,
            startWeekend,
            endWeekend, 
            dayRest);
        officeDay.GenerateWeekend();
        Assert.Empty(officeDay.OfficeHours);
    }
    
    [Fact]
    public void Should_Become_Unavailable_When_Cancelled()
    {
        var officeDay = new OfficeDay(new DateTime(2022, 12, 31), 30, new TimeSpan(8, 0, 0), new TimeSpan(18, 0, 0), new TimeSpan(8, 0, 0), new TimeSpan(12, 0, 0), DayOfWeek.Sunday);
        officeDay.Cancel();
        Assert.False(officeDay.IsAttending);
    }

    [Fact]
    public void Should_Become_Available_When_Attended()
    {
        var officeDay = new OfficeDay(new DateTime(2022, 12, 31), 30, new TimeSpan(8, 0, 0), new TimeSpan(18, 0, 0), new TimeSpan(8, 0, 0), new TimeSpan(12, 0, 0), DayOfWeek.Sunday);
        officeDay.Cancel();
        officeDay.Attend();
        Assert.True(officeDay.IsAttending);
    }
}