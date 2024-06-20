using E7BeautyShop.Appointment.Core.Entities;
using E7BeautyShop.Appointment.Core.Validations;

namespace E7BeautyShop.Appointment.Core.Services;

public class ValidatorTimeScheduling
{
    private IReadOnlyCollection<OfficeHour> OfficeHoursOrdered { get; }
    private OfficeHour TimeToSchedule { get; }

    public OfficeHour? PrevTime => OfficeHoursOrdered.LastOrDefault(of => of.DateAndHour < TimeToSchedule.DateAndHour);
    public OfficeHour? NextTime => OfficeHoursOrdered.FirstOrDefault(of => of.DateAndHour > TimeToSchedule.DateAndHour);

    public ValidatorTimeScheduling(IReadOnlyCollection<OfficeHour> officeHours, OfficeHour timeToSchedule)
    {
        ArgumentNullException.ThrowIfNull(nameof(officeHours));
        ArgumentNullException.ThrowIfNull(nameof(timeToSchedule));
        OfficeHoursOrdered = officeHours.OrderBy(of => of.DateAndHour).ToList().AsReadOnly();
        TimeToSchedule = timeToSchedule;
    }

    public bool Validate()
    {
        BusinessException.When(!ValidateTime, "Time to schedule is invalid");
        return true;
    }

    private bool ValidateTime => HasUniqueTime() ? HasUniqueTimeValid() : HasAtLeastTwoValid();

    private bool HasUniqueTimeValid()
    {
        if (TimeToSchedule.DateAndHour == OfficeHoursOrdered.First().DateAndHour)
        {
            throw new BusinessException("Time to schedule can not be equal to the current time");
        }

        if (TimeToScheduleLessThanCurrentTime())
        {
            return TimeToSchedulePlusDurationLessThanFirstCurrentTime();
        }

        if (TimeToScheduleGreaterThanCurrentTime())
        {
            return TimeToScheduleGreaterThanLastTimePlusDuration();
        }

        return false;
    }

    private bool HasAtLeastTwoValid()
    {
        if (TimeToScheduleLessThanCurrentTime())
        {
            return TimeToSchedule.PlusDuration() <= OfficeHoursOrdered.First().DateAndHour;
        }

        if (TimeToScheduleGreaterThanCurrentTime())
        {
            return TimeToSchedule.DateAndHour >= OfficeHoursOrdered.Last().PlusDuration();
        }

        if (TimeToScheduleGreaterThanPrevTime() && TimeToScheduleLessThanNextTime())
        {
            return TimeToSchedulePlusDurationLessThanNextTime() || PrevTimePlusDurationLessThanTimeToSchedule();
        }


        return false;
    }

    private bool HasAtLeastTwo()
    {
        return OfficeHoursOrdered.Count >= 2;
    }

    private bool TimeToScheduleGreaterThanLastTimePlusDuration()
    {
        return TimeToSchedule.DateAndHour >= OfficeHoursOrdered.Last().PlusDuration();
    }

    private bool TimeToSchedulePlusDurationLessThanFirstCurrentTime()
    {
        return TimeToSchedule.PlusDuration() <= OfficeHoursOrdered.First().DateAndHour;
    }

    private bool PrevTimePlusDurationLessThanTimeToSchedule()
    {
        return PrevTime?.PlusDuration() <= TimeToSchedule.DateAndHour;
    }

    private bool TimeToSchedulePlusDurationLessThanNextTime()
    {
        return TimeToSchedule.PlusDuration() <= NextTime?.DateAndHour;
    }

    private bool TimeToScheduleLessThanNextTime()
    {
        return TimeToSchedule.DateAndHour < NextTime?.DateAndHour;
    }

    private bool TimeToScheduleGreaterThanPrevTime()
    {
        return TimeToSchedule.DateAndHour > PrevTime?.DateAndHour;
    }

    private bool TimeToScheduleGreaterThanCurrentTime()
    {
        return TimeToSchedule.DateAndHour > OfficeHoursOrdered.Last().DateAndHour;
    }

    private bool TimeToScheduleLessThanCurrentTime()
    {
        return TimeToSchedule.DateAndHour < OfficeHoursOrdered.First().DateAndHour;
    }

    private bool HasUniqueTime()
    {
        return OfficeHoursOrdered.Count == 1;
    }
}