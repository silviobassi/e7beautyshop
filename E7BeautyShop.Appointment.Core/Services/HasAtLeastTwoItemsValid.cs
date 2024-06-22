using E7BeautyShop.Appointment.Core.Entities;

namespace E7BeautyShop.Appointment.Core.Services;

public sealed class HasAtLeastTwoItemsValid(
    IReadOnlyCollection<OfficeHour> officeHoursScheduled,
    OfficeHour timeToSchedule)
    : AbstractValidatorTimeToSchedule(officeHoursScheduled, timeToSchedule)
{
    private OfficeHour? PrevTime =>
        OfficeHourScheduled.LastOrDefault(of => of.DateAndHour < TimeToSchedule.DateAndHour);

    private OfficeHour? NextTime =>
        OfficeHourScheduled.FirstOrDefault(of => of.DateAndHour > TimeToSchedule.DateAndHour);

    public override bool Validate()
    {
        if (OfficeHourScheduled.Count < 2) return false;
        return IsFirstConditionValid() || IsSecondConditionValid() || IsThirdConditionValid();
    }

    private bool IsFirstConditionValid() =>
        IsTimeToScheduleLessThanCurrentTime && IsTimeToSchedulePlusDurationLessThanNextTime;

    private bool IsSecondConditionValid() =>
        IsTimeToScheduleGreaterThanCurrentTime && IsTimeToScheduleGreaterThanPrevTimePlusDuration;

    private bool IsThirdConditionValid() => IsTimeToScheduleGreaterThanPrevTime && IsTimeToScheduleLessThanNextTime &&
                                            IsTimeToSchedulePlusDurationLessThanNextTime &&
                                            IsPrevTimePlusDurationLessThanTimeToSchedule;

    private bool IsTimeToSchedulePlusDurationLessThanNextTime => TimeToSchedule.PlusDuration() <= NextTime?.DateAndHour;

    private bool IsTimeToScheduleGreaterThanPrevTimePlusDuration =>
        TimeToSchedule.DateAndHour >= OfficeHourScheduled.Last().PlusDuration();

    private bool IsTimeToScheduleGreaterThanPrevTime => TimeToSchedule.DateAndHour > PrevTime?.DateAndHour;
    private bool IsTimeToScheduleLessThanNextTime => TimeToSchedule.DateAndHour < NextTime?.DateAndHour;
    private bool IsPrevTimePlusDurationLessThanTimeToSchedule => PrevTime?.PlusDuration() <= TimeToSchedule.DateAndHour;
}