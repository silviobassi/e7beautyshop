using E7BeautyShop.Appointment.Core.Entities;
using E7BeautyShop.Appointment.Core.Validations;

namespace E7BeautyShop.Appointment.Core.Services;

public class ValidatorTimeScheduling
{
    public readonly int MinimumDuration = 30;
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
        BusinessException.When(IsNotTimeValid, "Time to schedule is invalid");
        return true;
    }

    private bool IsNotTimeValid => !IsTimeValid();

    private bool IsTimeValid()
    {
        if (HasUniqueTime())
            return HasUniqueTimeValid();
        if (HasAtLeastTwo)
            return HasAtLeastTwoValid();
        return OfficeHoursIsEmpty && IsTimeScheduleDurationAllowed;
    }
    
    // Método movido para outra classe 🎯
    private bool HasUniqueTimeValid()
    {
        if (TimeToScheduleLessThanCurrentTime()) return TimeToSchedulePlusDurationLessThanFirstCurrentTime();
        return TimeToScheduleGreaterThanCurrentTime() && TimeToScheduleGreaterThanLastTimePlusDuration;
    }

    
    // Método movido para outra classe 🎯
    private bool HasAtLeastTwoValid()
    {
        if (TimeToScheduleLessThanCurrentTime())
            return TimeToSchedulePlusDurationLessThanNextTime;
   
        if (TimeToScheduleGreaterThanCurrentTime())
            return TimeToSchedule.DateAndHour >= OfficeHoursOrdered.Last().PlusDuration();
        if (TimeToScheduleGreaterThanPrevTime() && TimeToScheduleLessThanNextTime())
            return TimeToSchedulePlusDurationLessThanNextTime || PrevTimePlusDurationLessThanTimeToSchedule;
        return false;
    }

    private bool HasAtLeastTwo => OfficeHoursOrdered.Count >= 2;

    private bool TimeToScheduleGreaterThanLastTimePlusDuration =>
        TimeToSchedule.DateAndHour >= OfficeHoursOrdered.Last().PlusDuration();

    private bool TimeToSchedulePlusDurationLessThanFirstCurrentTime()
        => TimeToSchedule.PlusDuration() <= OfficeHoursOrdered.First().DateAndHour;

    private bool PrevTimePlusDurationLessThanTimeToSchedule => PrevTime?.PlusDuration() <= TimeToSchedule.DateAndHour;

    private bool TimeToSchedulePlusDurationLessThanNextTime => TimeToSchedule.PlusDuration() <= NextTime?.DateAndHour;

    private bool TimeToScheduleLessThanNextTime() => TimeToSchedule.DateAndHour < NextTime?.DateAndHour;

    private bool TimeToScheduleGreaterThanPrevTime() => TimeToSchedule.DateAndHour > PrevTime?.DateAndHour;

    private bool TimeToScheduleGreaterThanCurrentTime() =>
        TimeToSchedule.DateAndHour > OfficeHoursOrdered.Last().DateAndHour;

    private bool TimeToScheduleLessThanCurrentTime() =>
        TimeToSchedule.DateAndHour < OfficeHoursOrdered.First().DateAndHour;
    private bool HasUniqueTime() => OfficeHoursOrdered.Count == 1;
    private bool OfficeHoursIsEmpty => OfficeHoursOrdered.Count == 0;
    private bool IsTimeScheduleDurationAllowed => TimeToSchedule.Duration >= MinimumDuration;
    
}