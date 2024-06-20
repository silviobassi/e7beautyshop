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
        BusinessException.When(!HasUniqueTimeValid(), "Time to schedule is invalid");
        return true;
    }

    private bool HasUniqueTimeValid()
    {
        if (HasUniqueTime())
        {
            if (TimeToSchedule.DateAndHour == OfficeHoursOrdered.First().DateAndHour)
            {
                throw new BusinessException("Time to schedule can not be equal to the current time");
            }

            if (TimeToScheduleLessThanCurrentTime())
            {
                return TimeToSchedule.PlusDuration() <= OfficeHoursOrdered.First().DateAndHour;
            }

            if (TimeToSchedule.DateAndHour > OfficeHoursOrdered.First().DateAndHour)
            {
                return TimeToSchedule.DateAndHour >= OfficeHoursOrdered.Last().PlusDuration();
            }
        }

        if (OfficeHoursOrdered.Count >= 2)
        {
            if (TimeToScheduleLessThanCurrentTime())
            {
                return TimeToSchedule.PlusDuration() <= OfficeHoursOrdered.First().DateAndHour;
            }

            if (TimeToSchedule.DateAndHour > OfficeHoursOrdered.Last().DateAndHour)
            {
                return TimeToSchedule.DateAndHour >= OfficeHoursOrdered.Last().PlusDuration();
            }


            if (TimeToSchedule.DateAndHour > PrevTime?.DateAndHour &&
                TimeToSchedule.DateAndHour < NextTime?.DateAndHour)
            {
                return TimeToSchedule.PlusDuration() <= NextTime.DateAndHour ||
                       PrevTime.PlusDuration() <= TimeToSchedule.DateAndHour;
            }
        }

        return false;
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