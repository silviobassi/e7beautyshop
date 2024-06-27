using E7BeautyShop.AgendaService.Core.Entities;
using E7BeautyShop.AgendaService.Core.Validations;
using static E7BeautyShop.AgendaService.Core.Validations.Messages;

namespace E7BeautyShop.AgendaService.Core.Services;

public sealed class HasAtLeastTwoItemsValid(IReadOnlyCollection<OfficeHour> timesScheduled, OfficeHour newTime)
    : AbstractValidatorTimeToSchedule(timesScheduled, newTime)
{
    private OfficeHour? PrevTime => TimesScheduled.LastOrDefault(of => of.DateAndHour < NewTime.DateAndHour);

    private OfficeHour? NextTime => TimesScheduled.FirstOrDefault(of => of.DateAndHour > NewTime.DateAndHour);

    public override bool Validate()
    {
        if (TimesScheduled.Count < 2) return false;
        CheckFirstConditionValid();
        CheckSecondConditionValid();
        return IsThirdConditionValid();
    }

    private void CheckFirstConditionValid()
    {
        BusinessException.ThrowIf(IsLessThan && !IsLessOrEqualThanNext, NewTimeBeforeNextTime);
    }

    private void CheckSecondConditionValid()
    {
        BusinessException.ThrowIf(IsGreaterThan && !IsBiggerOrEqualPrev, NewTimeAfterPrevTime);
    }
        
    private bool IsThirdConditionValid() => IsTimeToScheduleBetweenPrevAndNextTime && IsScheduleWithinTimeBounds;
    private bool IsTimeToScheduleBetweenPrevAndNextTime => IsGreaterThanPrev && IsLessThanNext;
    private bool IsScheduleWithinTimeBounds => IsLessOrEqualThanNext && IsPrevLessOrEqualNext;
    private bool IsLessOrEqualThanNext => NewTime.PlusDuration() <= NextTime?.DateAndHour;
    private bool IsPrevLessOrEqualNext => PrevTime?.PlusDuration() <= NewTime.DateAndHour;
    private bool IsBiggerOrEqualPrev => NewTime.DateAndHour >= TimesScheduled.Last().PlusDuration();
    private bool IsGreaterThanPrev => NewTime.DateAndHour > PrevTime?.DateAndHour;
    private bool IsLessThanNext => NewTime.DateAndHour < NextTime?.DateAndHour;
}