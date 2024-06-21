using E7BeautyShop.Appointment.Core.Entities;

namespace E7BeautyShop.Appointment.Core.Services;

public class HasUniqueValid : IValidator
{
    private readonly IReadOnlyCollection<OfficeHour> _officeHoursOrdered;
    private readonly OfficeHour _timeToSchedule;

    public HasUniqueValid(IReadOnlyCollection<OfficeHour> officeHours, OfficeHour timeToSchedule)
    {
        ArgumentNullException.ThrowIfNull(nameof(officeHours));
        ArgumentNullException.ThrowIfNull(nameof(timeToSchedule));
        _officeHoursOrdered = officeHours.OrderBy(of => of.DateAndHour).ToList().AsReadOnly();
        _timeToSchedule = timeToSchedule;
    }

    public bool Validate()
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