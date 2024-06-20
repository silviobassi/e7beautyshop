using E7BeautyShop.Appointment.Core.Entities;
using E7BeautyShop.Appointment.Core.Validations;

namespace E7BeautyShop.Appointment.Core.Services;

public class ValidatorTimeScheduling
{
    private IReadOnlyList<OfficeHour> OfficeHoursOrdered { get; }
    private OfficeHour TimeToSchedule { get; }

    public ValidatorTimeScheduling(IReadOnlyCollection<OfficeHour> officeHours, OfficeHour timeToSchedule)
    {
        ArgumentNullException.ThrowIfNull(officeHours);
        ArgumentNullException.ThrowIfNull(timeToSchedule);
        OfficeHoursOrdered = officeHours.OrderBy(of => of.DateAndHour).ToList().AsReadOnly();
        TimeToSchedule = timeToSchedule;
    }

    // Get the previous time scheduled
    private OfficeHour? PreviousTimeScheduled =>
        OfficeHoursOrdered.LastOrDefault(of => of.DateAndHour < TimeToSchedule.DateAndHour);

    // Get the next time scheduled 
    private OfficeHour? NextTimeScheduled =>
        OfficeHoursOrdered.FirstOrDefault(of => of.DateAndHour > TimeToSchedule.DateAndHour);

    public bool Validate()
    {
        BusinessException.When(!HasValidTimeSchedule, "Time to schedule not allowed");
        return true;
    }

    private bool HasValidTimeSchedule => HasOnlyOneTimeScheduled ? ValidateOnlyOneTime : ValidateAtLeastTwoTime();
    
    private bool HasOnlyOneTimeScheduled => OfficeHoursOrdered.Count == 1;

    private bool ValidateOnlyOneTime => IsPreviousTimeScheduleValid || IsNextTimeScheduleValid;

    private bool IsPreviousTimeScheduleValid => TimeToSchedule.DateAndHour >= PreviousTimeScheduled?.GetEndTime();

    private bool IsNextTimeScheduleValid => TimeToSchedule.GetEndTime() <= NextTimeScheduled?.DateAndHour;

    private bool ValidateAtLeastTwoTime()
    {
        if (PreviousTimeScheduled == null || NextTimeScheduled == null)
            return false;
        return IsIntervalBetweenPreviousAndNextValid && IsTimeAndDurationToScheduleValid;
    }

    private bool IsIntervalBetweenPreviousAndNextValid =>
        NextTimeScheduled?.DateAndHour.Subtract(PreviousTimeScheduled!.GetEndTime()).TotalMinutes >= 30;

    private bool IsTimeAndDurationToScheduleValid => TimeToSchedule.GetEndTime().Minute <= 30;
}