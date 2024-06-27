using E7BeautyShop.AgendaService.Core.Entities;

namespace E7BeautyShop.AgendaService.Core.Services;

public sealed class HasAtLeastTwoItemsValid(
    IReadOnlyCollection<OfficeHour> timesScheduled,
    OfficeHour newTime)
    : AbstractValidatorTimeToSchedule(timesScheduled, newTime)
{
    private OfficeHour? PrevTime =>
        TimesScheduled.LastOrDefault(of => of.DateAndHour < TimeToSchedule.DateAndHour);

    private OfficeHour? NextTime =>
        TimesScheduled.FirstOrDefault(of => of.DateAndHour > TimeToSchedule.DateAndHour);

    public override bool Validate()
    {
        if (TimesScheduled.Count < 2) return false;
        return IsFirstConditionValid() || IsSecondConditionValid() || IsThirdConditionValid();
    }

    private bool IsFirstConditionValid() =>
        IsTimeScheduledBefore && IsTimeToSchedulePlusDurationLessThanNextTime;

    private bool IsSecondConditionValid() =>
        IsTimeScheduledAfter && IsTimeToScheduleGreaterThanPrevTimePlusDuration;

    private bool IsThirdConditionValid() => IsTimeToScheduleBetweenPrevAndNextTime && IsScheduleWithinTimeBounds;

    private bool IsTimeToScheduleBetweenPrevAndNextTime
        => IsTimeToScheduleGreaterThanPrevTime && IsTimeToScheduleLessThanNextTime;
    
    private bool IsScheduleWithinTimeBounds => IsTimeToSchedulePlusDurationLessThanNextTime &&
                                               IsPrevTimePlusDurationLessThanTimeToSchedule;

    private bool IsTimeToSchedulePlusDurationLessThanNextTime => TimeToSchedule.PlusDuration() <= NextTime?.DateAndHour;

    private bool IsTimeToScheduleGreaterThanPrevTimePlusDuration =>
        TimeToSchedule.DateAndHour >= TimesScheduled.Last().PlusDuration();

    private bool IsTimeToScheduleGreaterThanPrevTime => TimeToSchedule.DateAndHour > PrevTime?.DateAndHour;
    private bool IsTimeToScheduleLessThanNextTime => TimeToSchedule.DateAndHour < NextTime?.DateAndHour;
    private bool IsPrevTimePlusDurationLessThanTimeToSchedule => PrevTime?.PlusDuration() <= TimeToSchedule.DateAndHour;
}