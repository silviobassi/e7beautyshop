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

    // Check if there is only one schedule
    public bool HasOnlyOneTimeScheduled => OfficeHoursOrdered.Count == 1;

    // Check if there is not only one schedule
    public bool HasNotOnlyOneOfficeHourScheduled => !HasOnlyOneTimeScheduled;

    // Check if there are two schedules
    public bool HasAtLeastTwoTimeScheduled => OfficeHoursOrdered.Count >= 2;

    public bool IsBiggerOrEqualTo30BetweenPrevNext =>
        NextTimeScheduled.DateAndHour.Subtract(PreviousTimeScheduled.GetEndTime()).TotalMinutes >= 30;


    // Check if there is a scheduling before the current scheduling
    public bool IsPreviousOfficeHourScheduled()
    {
        BusinessException.When(!HasOnlyOneTimeScheduled, "There is not only one OfficeHour scheduled");
        return OfficeHoursOrdered.Exists(of => of.DateAndHour < TimeToSchedule.DateAndHour);
    }

    // Check if there is a scheduling after the current scheduling
    public bool IsNextOfficeHourScheduled()
    {
        BusinessException.When(!HasOnlyOneTimeScheduled, "There is not only one OfficeHour scheduled");
        return OfficeHoursOrdered.Exists(of => of.DateAndHour > TimeToSchedule.DateAndHour);
    }

    // Check if the time to be scheduled is greater than the previous scheduling time by adding the date and time + duration
    public bool IsTimeScheduleGreaterOrEqualThanTimeAndDurationPrevious()
    {
        BusinessException.When(!HasOnlyOneTimeScheduled, "There is not only one OfficeHour scheduled");
        BusinessException.When(IsNextOfficeHourScheduled(), "There is a next OfficeHour scheduled");
        return OfficeHoursOrdered.Exists(of => TimeToSchedule.DateAndHour >= of.GetEndTime());
    }

    // Check if the time to be scheduled is less than previous scheduling time
    public bool IsTimeAndDurationLessOrEqualThanTimeSchedule()
    {
        BusinessException.When(!HasOnlyOneTimeScheduled, "There is not only one OfficeHour scheduled");
        BusinessException.When(IsPreviousOfficeHourScheduled(), "There is a previous OfficeHour scheduled");
        return OfficeHoursOrdered.Exists(of => TimeToSchedule.GetEndTime() <= of.DateAndHour);
    }

    public bool IsTimeAndDurationToScheduleLessThan30 => TimeToSchedule.GetEndTime().Minute <= 30;

    public bool Validate()
    {
        // validation to one scheduling
        BusinessException.When(HasNotOnlyOneOfficeHourScheduled, "There is not only one OfficeHour scheduled");

        var previousAndTimeGreaterOrEqual = IsPreviousOfficeHourScheduled() &&
                                            IsTimeScheduleGreaterOrEqualThanTimeAndDurationPrevious();
        var nextTimeAndDurationLessOrEqual =
            IsNextOfficeHourScheduled() && IsTimeAndDurationLessOrEqualThanTimeSchedule();

        var validateOnlyOneTime = HasOnlyOneTimeScheduled &&
                                  (previousAndTimeGreaterOrEqual || nextTimeAndDurationLessOrEqual);

        // validation to two or more scheduling
        var validateAtLeastTwoTime = HasAtLeastTwoTimeScheduled && IsBiggerOrEqualTo30BetweenPrevNext && IsTimeAndDurationToScheduleLessThan30;
        
        BusinessException.When(!validateOnlyOneTime && !validateAtLeastTwoTime, "Time to schedule not allowed");

        return true;
    }
}