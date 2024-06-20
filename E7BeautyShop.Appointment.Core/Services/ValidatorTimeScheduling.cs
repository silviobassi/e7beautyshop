using E7BeautyShop.Appointment.Core.Entities;
using E7BeautyShop.Appointment.Core.Validations;

namespace E7BeautyShop.Appointment.Core.Services;

public class ValidatorTimeScheduling
{
    private IReadOnlyCollection<OfficeHour> OfficeHoursOrdered { get; }
    private OfficeHour TimeToSchedule { get; }

    public ValidatorTimeScheduling(IReadOnlyCollection<OfficeHour> officeHours, OfficeHour timeToSchedule)
    {
        ArgumentNullException.ThrowIfNull(nameof(officeHours));
        ArgumentNullException.ThrowIfNull(nameof(timeToSchedule));
        OfficeHoursOrdered = officeHours.OrderBy(of => of.DateAndHour).ToList().AsReadOnly();
        TimeToSchedule = timeToSchedule;
    }

    public bool Validate()
    {
        BusinessException.When(!HasUniqueTimeValid(), "Time to schedule is invalid");
        return true;
    }

    private bool HasUniqueTimeValid ()
    {
        var result = HasUniqueTime && (OfficeHoursOrdered.First().DateAndHour > TimeToSchedule.DateAndHour ||
                                              OfficeHoursOrdered.First().DateAndHour < TimeToSchedule.DateAndHour);
        BusinessException.When(!result, "Time to schedule can not be equal to the first or last time");
        return result;
    }

    private bool HasAtLeastTwoTimesValid => HasAtLeastTwoTimes && IsGreaterThanPreviousTime && IsLessThanNextTime;

    private bool HasTimeLessThanFirstAndLastTimeValid =>
        (IsLessThanFirstTime && IsTimePlusDurationLessThanNext) || IsGreaterThanLastTime;

    private bool HasUniqueTime => OfficeHoursOrdered.Count == 1;

    private bool HasAtLeastTwoTimes => OfficeHoursOrdered.Count >= 2;

    private bool IsGreaterThanPreviousTime =>
        PreviousTime is not null && TimeToSchedule.DateAndHour > PreviousTime.DateAndHour;

    private bool IsLessThanNextTime => NextTime is not null && TimeToSchedule.DateAndHour < NextTime.DateAndHour;

    private OfficeHour? PreviousTime =>
        OfficeHoursOrdered.FirstOrDefault(of => of.DateAndHour < TimeToSchedule.DateAndHour);

    private OfficeHour? NextTime =>
        OfficeHoursOrdered.FirstOrDefault(of => of.DateAndHour > TimeToSchedule.DateAndHour);

    private bool IsLessThanFirstTime => TimeToSchedule.DateAndHour <= OfficeHoursOrdered.First().DateAndHour;

    private bool IsGreaterThanLastTime => TimeToSchedule.DateAndHour >= OfficeHoursOrdered.Last().DateAndHour;

    private bool IsTimePlusDurationLessThanNext =>
        NextTime is not null && TimeToSchedule.GetEndTime() <= NextTime.DateAndHour;

    private bool IsPreviousPlusLessThanTime =>
        PreviousTime is not null && PreviousTime.GetEndTime() <= TimeToSchedule.DateAndHour;
}