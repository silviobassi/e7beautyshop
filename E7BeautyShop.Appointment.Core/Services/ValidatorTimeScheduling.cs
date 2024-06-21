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
            throw new BusinessException("Time to schedule can not be equal to the current time");
        if (TimeToScheduleLessThanCurrentTime()) return TimeToSchedulePlusDurationLessThanFirstCurrentTime();
        return TimeToScheduleGreaterThanCurrentTime() && TimeToScheduleGreaterThanLastTimePlusDuration;
    }

    private bool HasAtLeastTwoValid()
    {
        /*
         * Criar classes
         * @TimeToScheduleLessThanCurrentTime, @TimeToScheduleGreaterThanCurrentTime, @TimeToScheduleGreaterThanPrevTime
         */
        if (TimeToScheduleLessThanCurrentTime())
            return TimeToSchedule.PlusDuration() <= OfficeHoursOrdered.First().DateAndHour;
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

    // Verificar se o intervalo tem ao menos 30 minutos para um time to schedule
    // Melhorar este método 👇!?
    private bool IntervalBetweenTimes()
    {
        if(NextTime == null || PrevTime == null) return false;
        return NextTime.DateAndHour.Subtract(PrevTime.DateAndHour).TotalMinutes >= 30;
    } 
    
}