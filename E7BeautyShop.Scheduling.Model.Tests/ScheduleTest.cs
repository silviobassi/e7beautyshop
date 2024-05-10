using Xunit.Abstractions;

namespace E7BeautyShop.Domain.Tests;

public class ScheduleTest
{
    private readonly Schedule _schedule;
    private readonly DateOnly _startAt = new(2021, 10, 1);

    private readonly TimeSpan _startAtWeekday = new(8, 0, 0);
    private readonly TimeSpan _endAtWeekday = new(16, 0, 0);
    private readonly TimeSpan _startAtHoliday = new(8, 0, 0);
    private readonly TimeSpan _endAtHoliday = new(20, 0, 0);
    private readonly TimeSpan _startAtWeekend = new(8, 0, 0);
    private readonly TimeSpan _endAtWeekend = new(20, 0, 0);
    
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
            new OfficeHoursOnWeekday(_startAtWeekday, _endAtWeekday),
            new OfficeHoursOnHoliday(_startAtHoliday, _endAtHoliday),
            new OfficeHoursOnWeekend(_startAtWeekend, _endAtWeekend),
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
        Assert.Equal(_startAtWeekday, _schedule.OfficeHoursOnWeekday.StartAt);
        Assert.Equal(_endAtWeekday, _schedule.OfficeHoursOnWeekday.EndAt);
        Assert.Equal(_startAtHoliday, _schedule.OfficeHoursOnHoliday.StartAt);
        Assert.Equal(_endAtHoliday, _schedule.OfficeHoursOnHoliday.EndAt);
        Assert.Equal(_startAtWeekend, _schedule.OfficeHoursOnWeekend.StartAt);
        Assert.Equal(_endAtWeekend, _schedule.OfficeHoursOnWeekend.EndAt);
        Assert.NotNull(_schedule.OfficeHoursOnHoliday);
        Assert.NotNull(_schedule.OfficeHoursOnWeekend);
        Assert.Equal(scheduleDurationInMonths, _schedule.ScheduleDurationInMonths);
        Assert.Equal(intervalo, _schedule.Interval);
        Assert.Equal(diasDeDescanso, _schedule.DiasDeDescanso);
    }
    
    [Fact]
    public void AddHoursForScheduling_ShouldReturnTrue_WhenHourIsAddedSuccessfully()
    {
        var hourForScheduling = new HourForScheduling(new TimeSpan(8, 0, 0));
        _schedule.AddHoursForScheduling(hourForScheduling);
        Assert.Single(_schedule.HoursForScheduling);
    }

    [Fact]
    public void AddHoursForScheduling_ShouldReturnFalse_WhenHourAlreadyExists()
    {
        var hourForScheduling = new HourForScheduling(new TimeSpan(8, 0, 0));
        var result1 = _schedule.AddHoursForScheduling(hourForScheduling);
        var result2 = _schedule.AddHoursForScheduling(hourForScheduling);
        Assert.True(result1);
        Assert.False(result2);
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
            _output.WriteLine($"Horários de Atendimento: {hourScheduling.Hour}");
        }
    }
}