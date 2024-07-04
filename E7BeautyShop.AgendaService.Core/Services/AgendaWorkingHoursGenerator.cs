using E7BeautyShop.AgendaService.Core.Entities;
using E7BeautyShop.AgendaService.Core.ObjectsValue;

namespace E7BeautyShop.AgendaService.Core.Services;

public class AgendaWorkingHoursGenerator
{
    /// <summary>
    /// Gets the start time for office hours on a given day.
    /// </summary>
    public TimeSpan? StartTime { get; private set; }

    /// <summary>
    /// Gets the end time for office hours on a given day.
    /// </summary>
    public TimeSpan? EndTime { get; private set; }

    /// <summary>
    /// Gets the current date being processed for office hours generation.
    /// </summary>
    public DateTime CurrentDate { get; private set; }

    /// <summary>
    /// Generates office hours for each day within the agenda's start and end dates.
    /// </summary>
    /// <param name="agenda">The agenda for which to generate office hours.</param>
    public void Generate(Agenda agenda)
    {
        ArgumentNullException.ThrowIfNull(nameof(agenda.Weekday));
        ArgumentNullException.ThrowIfNull(nameof(agenda.Weekend));
        for (CurrentDate = agenda.StartAt; CurrentDate < agenda.EndAt; CurrentDate = CurrentDate.AddDays(1))
        {
            SetOfficeHours(agenda);
        }
    }

    /// <summary>
    /// Sets the start and end times for office hours on the current date, based on whether it's a weekday or weekend.
    /// </summary>
    /// <param name="agenda">The agenda containing the weekday and weekend configurations.</param>
    private void SetOfficeHours(Agenda agenda)
    {
        var officeHour = OfficeHour.Create(CurrentDate, 30);
        WeekDayOrWeekend schedule = (officeHour.IsWeekday ? agenda.Weekday : agenda.Weekend)!;
        StartTime = schedule.StartAt;
        EndTime = schedule.EndAt;
    }
}