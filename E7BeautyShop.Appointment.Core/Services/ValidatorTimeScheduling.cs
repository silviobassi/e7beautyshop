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

    private bool HasUniqueTimeValid()
    {
        if (OfficeHoursOrdered.Count == 1)
        {
            
            if(TimeToSchedule.DateAndHour == OfficeHoursOrdered.First().DateAndHour)
            {
                throw new BusinessException("Time to schedule can not be equal to the current time");
            }
            if (TimeToSchedule.DateAndHour < OfficeHoursOrdered.First().DateAndHour)
            {
                return TimeToSchedule.PlusDuration() <= OfficeHoursOrdered.First().DateAndHour;
            }

            if (TimeToSchedule.DateAndHour > OfficeHoursOrdered.First().DateAndHour)
            {
                return TimeToSchedule.DateAndHour >= OfficeHoursOrdered.First().PlusDuration();
            }
        }

        if (OfficeHoursOrdered.Count >= 2)
        {
            if (TimeToSchedule.DateAndHour < OfficeHoursOrdered.First().DateAndHour)
            {
                return TimeToSchedule.PlusDuration() <= OfficeHoursOrdered.First().DateAndHour;
            }
            /*if(TimeToSchedule.DateAndHour > OfficeHoursOrdered.Last().DateAndHour)
            {
                return TimeToSchedule.DateAndHour >= OfficeHoursOrdered.Last().PlusDuration();
            }*/
        }

        return false;
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
        NextTime is not null && TimeToSchedule.PlusDuration() <= NextTime.DateAndHour;

    private bool IsPreviousPlusLessThanTime =>
        PreviousTime is not null && PreviousTime.PlusDuration() <= TimeToSchedule.DateAndHour;
}