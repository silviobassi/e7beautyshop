using Xunit.Abstractions;

namespace E7BeautyShop.Domain.Tests;

public class ScheduleTest
{
    private readonly Schedule _schedule;
    private readonly DateOnly _startAt = new(2021, 10, 1);

    private readonly TimeSpan _officeHoursStartAt = new(8, 0, 0);
    private readonly TimeSpan _officeHoursEndAt = new(16, 0, 0);

    private readonly ITestOutputHelper _output;

    public ScheduleTest(ITestOutputHelper output)

    {
        _output = output;

        const int scheduleDurationInMonths = 1;
        const int intervalo = 20;
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
            _startAt,
            new OfficeHours(_officeHoursStartAt, _officeHoursEndAt),
            scheduleDurationInMonths, intervalo, diasDeDescanso);
    }

    [Fact]
    public void ShouldCreate_Schedule()
    {
        const int scheduleDurationInMonths = 1;
        const int intervalo = 20;

        List<string> diasDeDescanso =
        [
            DayOfWeek.Monday.ToString(),
            DayOfWeek.Tuesday.ToString(),
            DayOfWeek.Wednesday.ToString(),
            DayOfWeek.Thursday.ToString(),
            DayOfWeek.Friday.ToString(),
            DayOfWeek.Saturday.ToString()
        ];
        
        Assert.Equal(_startAt, _schedule.StartAt);
        Assert.Equal(_officeHoursStartAt, _schedule.OfficeHours.StartAt);
        Assert.Equal(_officeHoursEndAt, _schedule.OfficeHours.EndAt);
        Assert.Equal(scheduleDurationInMonths, _schedule.ScheduleDurationInMonths);
        Assert.Equal(intervalo, _schedule.Interval);
        Assert.Equal(diasDeDescanso, _schedule.DaysRest);
    }

    [Fact]
    public void AddHoursForScheduling_ShouldReturnTrue_WhenHourIsAddedSuccessfully()
    {
        var hourForScheduling = new HourForScheduling().CreateHourWeekday(new TimeSpan(8, 0, 0));
        _schedule.AddHoursForScheduling(hourForScheduling);
        Assert.Single(_schedule.HoursForScheduling);
    }

    [Fact]
    public void Should_AddOfficeHoursOnWeekday()
    {
        _schedule.Generate();
        var hoursScheduling = _schedule.HoursForScheduling;
        Assert.Equal(25, hoursScheduling.Count);
        foreach (var hourScheduling in hoursScheduling)
        {
            _output.WriteLine($"Horários de Atendimento: {hourScheduling.HourWeekday}");
        }
    }
}