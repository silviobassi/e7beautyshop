using E7BeautyShop.AgendaService.Core.Entities;

namespace E7BeautyShop.AgendaService.Core.Services;

public sealed class HasUniqueItemValid(IReadOnlyCollection<OfficeHour> officeHoursScheduled, OfficeHour timeToSchedule)
    : AbstractValidatorTimeToSchedule(officeHoursScheduled, timeToSchedule)
{
    public override bool Validate()
    {
        if (OfficeHourScheduled.Count != 1) return false;
        if (IsTimeToScheduleLessThanCurrentTime) return IsTimeToSchedulePlusDurationLessThanFirstCurrentTime;
        return IsTimeToScheduleGreaterThanCurrentTime && IsTimeToScheduleGreaterThanLastTimePlusDuration;
    }

    private bool IsTimeToSchedulePlusDurationLessThanFirstCurrentTime
        => TimeToSchedule.PlusDuration() <= OfficeHourScheduled.First().DateAndHour;
    
    private bool IsTimeToScheduleGreaterThanLastTimePlusDuration =>
        TimeToSchedule.DateAndHour >= OfficeHourScheduled.Last().PlusDuration();
}