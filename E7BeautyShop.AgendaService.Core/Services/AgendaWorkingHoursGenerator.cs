using E7BeautyShop.AgendaService.Core.Entities;
using E7BeautyShop.AgendaService.Core.ObjectsValue;

namespace E7BeautyShop.AgendaService.Core.Services;

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
        for (var time = WeekDayOrEnd?.StartAt;
             time < WeekDayOrEnd?.EndAt;
             time = time.Value.Add(TimeSpan.FromMinutes(30)))
        {
            var newTime = OfficeHour.Create(new DateTime(
                CurrentDate.Year, CurrentDate.Month, CurrentDate.Day, time.Value.Hours, time.Value.Minutes, 0,
                DateTimeKind.Utc), 30);

            agenda.AddOfficeHour(newTime);
        }
    }

    private bool IsWeekday => CurrentDate.DayOfWeek != DayOfWeek.Saturday && CurrentDate.DayOfWeek != DayOfWeek.Sunday;
}