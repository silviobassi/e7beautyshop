using E7BeautyShop.AgendaService.Core.Entities;
using E7BeautyShop.AgendaService.Core.Validations;
using static E7BeautyShop.AgendaService.Core.Validations.Messages;

namespace E7BeautyShop.AgendaService.Core.Services;

public sealed class HasUniqueItemValid(IReadOnlyCollection<OfficeHour> officeHoursScheduled, OfficeHour timeToSchedule)
    : AbstractValidatorTimeToSchedule(officeHoursScheduled, timeToSchedule)
{
    public override bool Validate()
    {
        if (OfficeHourScheduled.Count != 1) return false;
        
        BusinessException.When(
            IsTimeToScheduleLessThanCurrentTime && !IsTimeToSchedulePlusDurationLessThanFirstCurrentTime,
            TimeToScheduleCannotGreaterThanFirstCurrentTime);
        
        BusinessException.When(IsTimeToScheduleGreaterThanCurrentTime &&
                               !IsTimeToScheduleGreaterThanLastTimePlusDuration,
            TimeToScheduleCannotLessThanFirstCurrentTime);

        return false;
    }

    private bool IsTimeToSchedulePlusDurationLessThanFirstCurrentTime
        => TimeToSchedule.PlusDuration() <= OfficeHourScheduled.First().DateAndHour;

    private bool IsTimeToScheduleGreaterThanLastTimePlusDuration =>
        TimeToSchedule.DateAndHour >= OfficeHourScheduled.First().PlusDuration();
}