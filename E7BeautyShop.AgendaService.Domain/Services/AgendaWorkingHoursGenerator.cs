using E7BeautyShop.AgendaService.Domain.Entities;
using E7BeautyShop.AgendaService.Domain.ObjectsValue;
using E7BeautyShop.AgendaService.Domain.ValueObjects;

namespace E7BeautyShop.AgendaService.Domain.Services;

public class AgendaWorkingHoursGenerator(Agenda agenda)
{
    private DateTime CurrentDate { get; set; }
    private WeekDayOrWeekend? WeekDayOrEnd { get; set; }

    public void Generate()
    {
        for (CurrentDate = agenda.StartAt; CurrentDate <= agenda.EndAt; CurrentDate = CurrentDate.AddDays(1))
        {
            WeekDayOrEnd = (IsWeekday ? agenda.Weekday : agenda.Weekend)!;
            GetTimes();
        }
    }

    private void GetTimes()
    {
        ArgumentNullException.ThrowIfNull(WeekDayOrEnd);
        SetNewTime();
    }

    private void SetNewTime()
    {
        var startTime = WeekDayOrEnd!.StartAt;
        var endTime = WeekDayOrEnd!.EndAt;
        while (startTime < endTime)
        {
            var newTime = OfficeHour.Create(GetDateAndHour(startTime), 30);
            agenda.AddOfficeHour(newTime);
            startTime = startTime.Value.Add(TimeSpan.FromMinutes(30));
        }
    }

    private DateTime GetDateAndHour(TimeSpan? time) =>
        new DateTime(CurrentDate.Year, CurrentDate.Month, CurrentDate.Day, time!.Value.Hours, time!.Value.Minutes, 0,
            DateTimeKind.Utc);

    private bool IsWeekday => CurrentDate.DayOfWeek != DayOfWeek.Saturday && CurrentDate.DayOfWeek != DayOfWeek.Sunday;
}