using E7BeautyShop.Appointment.Core.Entities;

namespace E7BeautyShop.Appointment.Core.Services;

public sealed class HasAtLeastTwoValid(IReadOnlyCollection<OfficeHour> officeHours, OfficeHour timeToSchedule)
    : ValidatorAbstract(officeHours, timeToSchedule)
{
    private readonly IReadOnlyCollection<OfficeHour> _officeHoursOrdered;
    private readonly OfficeHour _timeToSchedule;

    private OfficeHour? PrevTime =>
        _officeHoursOrdered.LastOrDefault(of => of.DateAndHour < _timeToSchedule.DateAndHour);

    private OfficeHour? NextTime =>
        _officeHoursOrdered.FirstOrDefault(of => of.DateAndHour > _timeToSchedule.DateAndHour);

    public override bool Validate()
    {
        if (_officeHoursOrdered.Count < 2) return false;

        if (TimeToScheduleLessThanCurrentTime)
            return TimeToSchedulePlusDurationLessThanNextTime;

        if (TimeToScheduleGreaterThanCurrentTime)
            return _timeToSchedule.DateAndHour >= _officeHoursOrdered.Last().PlusDuration();

        if (TimeToScheduleGreaterThanPrevTime && TimeToScheduleLessThanNextTime)
            return TimeToSchedulePlusDurationLessThanNextTime || PrevTimePlusDurationLessThanTimeToSchedule;

        return false;
    }

    private bool TimeToScheduleLessThanCurrentTime =>
        _timeToSchedule.DateAndHour < _officeHoursOrdered.First().DateAndHour;

    private bool TimeToSchedulePlusDurationLessThanNextTime => _timeToSchedule.PlusDuration() <= NextTime?.DateAndHour;

    private bool TimeToScheduleGreaterThanCurrentTime =>
        _timeToSchedule.DateAndHour > _officeHoursOrdered.Last().DateAndHour;

    private bool TimeToScheduleGreaterThanPrevTime => _timeToSchedule.DateAndHour > PrevTime?.DateAndHour;

    private bool TimeToScheduleLessThanNextTime => _timeToSchedule.DateAndHour < NextTime?.DateAndHour;
    private bool PrevTimePlusDurationLessThanTimeToSchedule => PrevTime?.PlusDuration() <= _timeToSchedule.DateAndHour;
}