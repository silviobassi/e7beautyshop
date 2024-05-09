namespace E7BeautyShop.Domain.Tests;

public class ScheduleTest
{
    private readonly Schedule _schedule;
    private readonly TimeSpan _startAtWeekday = new TimeSpan(8, 0, 0);
    private readonly TimeSpan _endAtWeekday = new TimeSpan(16, 0, 0);
    private readonly TimeSpan _startAtHoliday = new TimeSpan(8, 0, 0);
    private readonly TimeSpan _endAtHoliday = new TimeSpan(20, 0, 0);
    private readonly TimeSpan _startAtWeekend = new TimeSpan(8, 0, 0);
    private readonly TimeSpan _endAtWeekend = new TimeSpan(20, 0, 0);

    public ScheduleTest()
    {
        //var diasDeTrabalho = ["Quarta", "Quinta", "Sexta" ];
        const int scheduleDurationInMonths = 1;
        const int intervalo = 30;
        List<string> diasDeDescanso =
        [
            DayOfWeek.Monday.ToString(),
            DayOfWeek.Tuesday.ToString(),
            DayOfWeek.Wednesday.ToString(),
            DayOfWeek.Thursday.ToString(),
            DayOfWeek.Friday.ToString(),
            DayOfWeek.Saturday.ToString()
        ];

        _schedule = new Schedule(
            new OfficeHoursOnWeekday(_startAtWeekday, _endAtWeekday),
            new OfficeHoursOnHoliday(_startAtHoliday, _endAtHoliday),
            new OfficeHoursOnWeekend(_startAtWeekend, _endAtWeekend),
            scheduleDurationInMonths, intervalo, diasDeDescanso);
    }

    [Fact]
    public void ShouldCreate_Schedule()
    {
        const int scheduleDurationInMonths = 1;
        const int intervalo = 30;

        List<string> diasDeDescanso =
        [
            DayOfWeek.Monday.ToString(),
            DayOfWeek.Tuesday.ToString(),
            DayOfWeek.Wednesday.ToString(),
            DayOfWeek.Thursday.ToString(),
            DayOfWeek.Friday.ToString(),
            DayOfWeek.Saturday.ToString()
        ];

        Assert.Equal(_startAtWeekday, _schedule.OfficeHoursOnWeekday.StartAt);
        Assert.Equal(_endAtWeekday, _schedule.OfficeHoursOnWeekday.EndAt);
        Assert.Equal(scheduleDurationInMonths, _schedule.ScheduleDurationInMonths);
        Assert.Equal(intervalo, _schedule.Intervalo);
        Assert.Equal(_startAtHoliday, _schedule.OfficeHoursOnHoliday.StartAt);
        Assert.Equal(_endAtHoliday, _schedule.OfficeHoursOnHoliday.EndAt);
        Assert.Equal(_startAtWeekend, _schedule.OfficeHoursOnWeekend.StartAt);
        Assert.Equal(diasDeDescanso, _schedule.DiasDeDescanso);
    }

    [Fact]
    public void Should_GenerateSchedule()
    {
        const int scheduleDurationInMonths = 1;
        const int intervalo = 30;

        List<string> diasDeDescanso =
        [
            DayOfWeek.Monday.ToString(),
            DayOfWeek.Tuesday.ToString(),
            DayOfWeek.Wednesday.ToString(),
            DayOfWeek.Thursday.ToString(),
            DayOfWeek.Friday.ToString(),
            DayOfWeek.Saturday.ToString()
        ];

        bool result = _schedule.Generate();
        Assert.True(result);
    }
}