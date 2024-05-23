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
        var dayRest = new List<DayOfWeek> { DayOfWeek.Sunday };
        var officeDays = new List<OfficeDay>();
        var professionalId = Guid.NewGuid();
        var customerId = Guid.NewGuid();
        var schedule = new Schedule(
            startAt,
            endAt,
            startWeekday,
            endWeekday,
            startWeekend,
            endWeekend,
            dayRest,
            officeDays,
            professionalId,
            customerId);
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
            var isWeekday = day.DateTime.DayOfWeek != DayOfWeek.Saturday && day.DateTime.DayOfWeek != schedule.DayRest[0];
            var start = isWeekday ? schedule.StartWeekday : schedule.StartWeekend;
            var end = isWeekday ? schedule.EndWeekday : schedule.EndWeekend;

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