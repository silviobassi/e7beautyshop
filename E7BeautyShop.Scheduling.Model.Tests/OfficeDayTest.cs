using Xunit.Abstractions;

namespace E7BeautyShop.Domain.Tests;

public class OfficeDayTest(ITestOutputHelper output)
{
    private static Weekend Weekend
    {
        get
        {
            var weekend = new Weekend(new TimeSpan(8, 0, 0), new TimeSpan(12, 0, 0));
            return weekend;
        }
    }

    private static Weekday Weekday
    {
        get
        {
            var weekday = new Weekday(new TimeSpan(8, 0, 0), new TimeSpan(18, 0, 0));
            return weekday;
        }
    }

    [Fact]
    public void Generate_WhenCalledOnWeekday_AddsOfficeHourToWeekday()
    {
        var officeDay = new OfficeDay(
            new DateTime(2024, 5, 23, 0, 0, 0, DateTimeKind.Local),
            Weekday, Weekend, DayOfWeek.Sunday);
        for (var i = Weekday.StartAt; i <= Weekday.EndAt; i += TimeSpan.FromMinutes(30))
        {
            var officeHour = new OfficeHour { Hour = i };
            officeDay.AddOfficeHour(officeHour);
        }

        Assert.Equal(21, officeDay.OfficeHours.Count);
    }


    [Fact]
    public void Generate_WhenCalledOnWeekend_AddsOfficeHourToWeekend()
    {
        var officeDay = new OfficeDay(
            new DateTime(2024, 5, 25, 0, 0, 0, DateTimeKind.Local),
            Weekday, Weekend, DayOfWeek.Sunday);
        for (var i = Weekend.StartAt; i <= Weekend.EndAt; i += TimeSpan.FromMinutes(30))
        {
            var officeHour = new OfficeHour { Hour = i };
            officeDay.AddOfficeHour(officeHour);
        }

        Assert.Equal(9, officeDay.OfficeHours.Count);
    }


    [Fact]
    public void Generate_CreateSchedule()
    {
        var startAt = new DateTime(2024, 5, 25, 0, 0, 0, DateTimeKind.Local);
        var officeDays = GenerateOfficeDays(startAt);
        Assert.Equal(30, officeDays.Count);
       
    }

    private static List<OfficeDay> GenerateOfficeDays(DateTime startAt)
    {
        List<OfficeDay> officeDays = [];
        for (var d = 0; d < 30; d++)
        {
            var officeDay = new OfficeDay(startAt.AddDays(d), Weekday, Weekend, DayOfWeek.Sunday);
            officeDays.Add(officeDay);
            
            var start = officeDay.IsNotWeekday ? Weekend.StartAt : Weekday.StartAt;
            var end = officeDay.IsNotWeekday ? Weekend.EndAt : Weekday.EndAt;
            
                for (var i = start; i <= end; i += TimeSpan.FromMinutes(30))
                {
                    var officeHour = new OfficeHour { Hour = i };
                    officeDay.AddOfficeHour(officeHour);
                }
        }

        return officeDays;
    }
}