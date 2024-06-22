using E7BeautyShop.Appointment.Core.Entities;

namespace E7BeautyShop.Appointment.Core.Services;

public sealed class HasUniqueValid(IReadOnlyCollection<OfficeHour> officeHours, OfficeHour timeToSchedule)
    : ValidatorAbstract(officeHours, timeToSchedule)
{
    public override bool Validate()
    {
        if (_officeHoursOrdered.Count != 1) return false;
        if (TimeToScheduleLessThanCurrentTime()) return TimeToSchedulePlusDurationLessThanFirstCurrentTime();
        return TimeToScheduleGreaterThanCurrentTime() && TimeToScheduleGreaterThanLastTimePlusDuration;
    }

    private bool TimeToSchedulePlusDurationLessThanFirstCurrentTime()
        => _timeToSchedule.PlusDuration() <= _officeHoursOrdered.First().DateAndHour;

    private bool TimeToScheduleLessThanCurrentTime() =>
        _timeToSchedule.DateAndHour < _officeHoursOrdered.First().DateAndHour;

    private bool TimeToScheduleGreaterThanCurrentTime() =>
        _timeToSchedule.DateAndHour > _officeHoursOrdered.Last().DateAndHour;

    private bool TimeToScheduleGreaterThanLastTimePlusDuration =>
        _timeToSchedule.DateAndHour >= _officeHoursOrdered.Last().PlusDuration();
}