using E7BeautyShop.Appointment.Core.Entities;
using E7BeautyShop.Appointment.Core.Validations;

namespace E7BeautyShop.Appointment.Core.Services;

public class ValidatorTimeScheduling
{
    private List<OfficeHour> OfficeHoursOrdered { get; set; }
    private OfficeHour TimeToSchedule { get; init; }

    public ValidatorTimeScheduling(IReadOnlyCollection<OfficeHour> officeHours, OfficeHour timeToSchedule)
    {
        ArgumentNullException.ThrowIfNull(nameof(officeHours));
        ArgumentNullException.ThrowIfNull(nameof(timeToSchedule));
        OfficeHoursOrdered = officeHours.OrderBy(of => of.DateAndHour).ToList();
        TimeToSchedule = timeToSchedule;
    }

    // Get the previous time scheduled
    public OfficeHour PreviousTimeScheduled =>
        OfficeHoursOrdered.First(of => of.DateAndHour < TimeToSchedule.DateAndHour)!;

    // Get the next time scheduled 
    public OfficeHour NextTimeScheduled =>
        OfficeHoursOrdered.Last(of => of.DateAndHour > TimeToSchedule.DateAndHour)!;

    public bool Validate()
    {
        BusinessException.When(HasNotOnlyOneOfficeHourScheduled, "There is not only one OfficeHour scheduled");
        BusinessException.When(!ValidateOnlyOneTime && !ValidateAtLeastTwoTime, "Time to schedule not allowed");
        return true;
    }

    // Check if there is not only one schedule
    private bool HasNotOnlyOneOfficeHourScheduled => !HasOnlyOneTimeScheduled;
    // Check if there is only one schedule
    
    private bool ValidateOnlyOneTime
        => HasOnlyOneTimeScheduled &&
           (IsPreviousTimeScheduleAndTimeScheduleBiggerOrEqualTimeAndDurationPrevious ||
            IsNextTimeScheduledAndTimeDurationLessOrEqualTimeSchedule);
    
    private bool HasOnlyOneTimeScheduled => OfficeHoursOrdered.Count == 1;
    
    private bool IsPreviousTimeScheduleAndTimeScheduleBiggerOrEqualTimeAndDurationPrevious
        => IsPreviousOfficeHourScheduled && IsTimeScheduleGreaterOrEqualThanTimeAndDurationPrevious;

    // Check if there is a scheduling before the current scheduling
    private bool IsPreviousOfficeHourScheduled
        => OfficeHoursOrdered.Exists(of => of.DateAndHour < TimeToSchedule.DateAndHour);

    // Check if the time to be scheduled is greater than the previous scheduling time by adding the date and time + duration
    private bool IsTimeScheduleGreaterOrEqualThanTimeAndDurationPrevious
        => OfficeHoursOrdered.Exists(of => TimeToSchedule.DateAndHour >= of.GetEndTime());

    private bool IsNextTimeScheduledAndTimeDurationLessOrEqualTimeSchedule =>
        IsNextOfficeHourScheduled && IsTimeAndDurationLessOrEqualThanTimeSchedule;

    // Check if there is a scheduling after the current scheduling
    private bool IsNextOfficeHourScheduled
        => OfficeHoursOrdered.Exists(of => of.DateAndHour > TimeToSchedule.DateAndHour);

    // Check if the time to be scheduled is less than previous scheduling time
    private bool IsTimeAndDurationLessOrEqualThanTimeSchedule
        => OfficeHoursOrdered.Exists(of => TimeToSchedule.GetEndTime() <= of.DateAndHour);

    private bool ValidateAtLeastTwoTime => HasAtLeastTwoTimeScheduled && IsBiggerOrEqualTo30BetweenPrevNext &&
                                           IsTimeAndDurationToScheduleLessThan30;

    // Check if there are two schedules
    private bool HasAtLeastTwoTimeScheduled => OfficeHoursOrdered.Count >= 2;

    private bool IsBiggerOrEqualTo30BetweenPrevNext =>
        NextTimeScheduled.DateAndHour.Subtract(PreviousTimeScheduled.GetEndTime()).TotalMinutes >= 30;


    private bool IsTimeAndDurationToScheduleLessThan30 => TimeToSchedule.GetEndTime().Minute <= 30;



}