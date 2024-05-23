using Xunit.Abstractions;

namespace E7BeautyShop.Schedule.Tests;

public class ScheduleTest(ITestOutputHelper output)
{
    [Fact]
    public void Should_CreateSchedule()
    {
        var startAt = new DateTime(2025, 5, 22, 0, 0, 0, DateTimeKind.Local);
        var endAt = new DateTime(2025, 5, 25, 0, 0, 0, DateTimeKind.Local);
        var startWeekday = new TimeSpan(8, 0, 0);
        var endWeekday = new TimeSpan(18, 0, 0);
        var startWeekend = new TimeSpan(8, 0, 0);
        var endWeekend = new TimeSpan(12, 0, 0);

        var weekday = new Weekday(startWeekday, endWeekday);
        Assert.NotNull(weekday);
        Assert.Equal(startWeekday, weekday.StartAt);
        Assert.Equal(endWeekday, weekday.EndAt);

        var weekend = new Weekend(startWeekend, endWeekend);
        Assert.NotNull(weekend);
        Assert.Equal(startWeekend, weekend.StartAt);
        Assert.Equal(endWeekend, weekend.EndAt);

        var dayRest = new List<DayRest> { new DayRest(DayOfWeek.Sunday)};
        var officeDays = new List<OfficeDay>();
        var professionalId = Guid.NewGuid();
        var customerId = Guid.NewGuid();
        var schedule = new Schedule(startAt, endAt, weekday, weekend, dayRest, officeDays, professionalId, customerId);
        
        Assert.NotNull(schedule);
        Assert.Equal(startAt, schedule.StartAt);
        Assert.Equal(endAt, schedule.EndAt);
        Assert.Empty(officeDays);
        Assert.Equal(professionalId, schedule.ProfessionalId);
        Assert.Equal(customerId, schedule.CustomerId);


        for (var i = schedule.StartAt; i < schedule.EndAt; i = i.AddDays(1))
        {
            schedule.AddOfficeDay(new OfficeDay(i));
        }

        foreach (var day in officeDays)
        {
            var isWeekday = day.DateTime.DayOfWeek != DayOfWeek.Saturday &&
                            day.DateTime.DayOfWeek != schedule.DayRest[0].DayOnWeek;
            var start = isWeekday ? schedule.Weekday.StartAt : schedule.Weekend.StartAt;
            var end = isWeekday ? schedule.Weekday.EndAt : schedule.Weekend.EndAt;

            for (var j = start; j <= end; j = j.Add(TimeSpan.FromMinutes(30)))
            {
                day.AddOfficeHour(new OfficeHour(j));
            }
        }

        Assert.NotEmpty(schedule.OfficeDays);
        Assert.Equal(3, schedule.OfficeDays.Count);
        //Assert.Equal(200, schedule.OfficeHours.Count);

        foreach (var day in schedule.OfficeDays)
        {
            output.WriteLine(day.DateTime.ToString("dd/MM/yyyy"));

            foreach (var hour in day.TimesOfDay)
            {
                output.WriteLine(hour.TimeOfDay.ToString());
            }
        }
    }
}