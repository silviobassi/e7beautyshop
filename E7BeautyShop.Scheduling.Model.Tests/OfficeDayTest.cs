using Xunit.Abstractions;

namespace E7BeautyShop.Domain.Tests;

public class OfficeDayTest()
{
    private const int Interval = 30;
    private static readonly TimeSpan StartWeekDay = new(8, 0, 0);
    private static readonly TimeSpan EndWeekday = new(18, 0, 0);
    private static readonly TimeSpan StartWeekend = new(8, 0, 0);
    private static readonly TimeSpan EndWeekend = new(12, 0, 0);
    private const DayOfWeek DayRest = DayOfWeek.Sunday;
    
    private readonly Weekday _weekday = new(StartWeekDay, EndWeekday);
    private readonly Weekend _weekend = new(StartWeekend, EndWeekend);
    
    [Fact]
    public void Should_Create_OfficeDay()
    {
        var officeDay = OfficeWeekday();
        Assert.NotNull(officeDay);
        Assert.Equal(new DateTime(2024, 5, 2), officeDay.Date);
        Assert.Equal(30, officeDay.Interval);
    }
    
    [Fact]
    public void Should_Generate_OfficeHours_Weekday()
    {
        var officeDay = OfficeWeekday();
        officeDay.GenerateWeekday();
        Assert.Equal(21, officeDay.OfficeHours.Count);
        Assert.Equal(StartWeekDay, officeDay.OfficeHours[0].Hour);
        Assert.Equal(EndWeekday, officeDay.OfficeHours[20].Hour);
    }

    [Fact]
    public void Should_EmptyReturn_To_OfficeHours_Weekday_When__dayRest_Equal_DateDay()
    {
        var officeDay = OfficeNotWeekday();
        officeDay.GenerateWeekday();
        Assert.Empty(officeDay.OfficeHours);
    }

   

    [Fact]
    public void Should_Generate__weekend()
    {
        var date = new DateTime(2024, 5, 11);
        OfficeDay officeDay = new(date, Interval, _weekday, _weekend, DayRest);
        officeDay.GenerateWeekend();
        Assert.Equal(9, officeDay.OfficeHours.Count);
        Assert.Equal(StartWeekend, officeDay.OfficeHours[0].Hour);
        Assert.Equal(EndWeekend, officeDay.OfficeHours[8].Hour);
    }

    [Fact]
    public void Should_EmptyReturn_To_OfficeHours__weekend_When__dayRest_Equal_DateDay()
    {
        var officeDay = OfficeNotWeekday();
        officeDay.GenerateWeekend();
        Assert.Empty(officeDay.OfficeHours);
    }

    [Fact]
    public void Should_Become_Unavailable_When_Cancelled()
    {
        var officeDay = OfficeNotWeekday();
        officeDay.Cancel();
        Assert.False(officeDay.IsAttending);
    }

    [Fact]
    public void Should_Become_Available_When_Attended()
    {
        var officeDay = OfficeNotWeekday();
        officeDay.Cancel();
        officeDay.Attend();
        Assert.True(officeDay.IsAttending);
    }
    
    private OfficeDay OfficeWeekday()
    {
        OfficeDay officeDay = new(new DateTime(2024, 5, 2), Interval, _weekday, _weekend, DayRest);
        return officeDay;
    }
    
    private OfficeDay OfficeNotWeekday()
    {
        const DayOfWeek dayRest = DayOfWeek.Monday;
        OfficeDay officeDay = new(new DateTime(2024, 5, 6), Interval, _weekday, _weekend, dayRest);
        return officeDay;
    }
}