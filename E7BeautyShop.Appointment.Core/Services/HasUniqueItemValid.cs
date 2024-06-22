using E7BeautyShop.Appointment.Core.Entities;

namespace E7BeautyShop.Appointment.Core.Services;

public sealed class HasUniqueItemValid(IReadOnlyCollection<OfficeHour> officeHoursScheduled, OfficeHour timeToSchedule)
    : AbstractValidatorTimeToSchedule(officeHoursScheduled, timeToSchedule)
{
    public override bool Validate()
    {
        if (OfficeHourScheduled.Count != 1) return false;
        if (TimeToScheduleLessThanCurrentTime()) return TimeToSchedulePlusDurationLessThanFirstCurrentTime();
        return TimeToScheduleGreaterThanCurrentTime() && TimeToScheduleGreaterThanLastTimePlusDuration;
    }

    private bool TimeToSchedulePlusDurationLessThanFirstCurrentTime()
        => TimeToSchedule.PlusDuration() <= OfficeHourScheduled.First().DateAndHour;

    private bool TimeToScheduleLessThanCurrentTime() =>
        TimeToSchedule.DateAndHour < OfficeHourScheduled.First().DateAndHour;

    private bool TimeToScheduleGreaterThanCurrentTime() =>
        TimeToSchedule.DateAndHour > OfficeHourScheduled.Last().DateAndHour;

    private bool TimeToScheduleGreaterThanLastTimePlusDuration =>
        TimeToSchedule.DateAndHour >= OfficeHourScheduled.Last().PlusDuration();
}