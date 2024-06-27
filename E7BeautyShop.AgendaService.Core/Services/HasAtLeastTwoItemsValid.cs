using E7BeautyShop.AgendaService.Core.Entities;
using E7BeautyShop.AgendaService.Core.Validations;
using static E7BeautyShop.AgendaService.Core.Validations.Messages;

namespace E7BeautyShop.AgendaService.Core.Services;

public sealed class HasAtLeastTwoItemsValid(IReadOnlyCollection<OfficeHour> timesScheduled, OfficeHour newTime)
    : AbstractValidatorTimeToSchedule(timesScheduled, newTime)
{
    private OfficeHour? PrevTime => TimesScheduled.LastOrDefault(of => of.DateAndHour < NewTime.DateAndHour);

    private OfficeHour? NextTime => TimesScheduled.FirstOrDefault(of => of.DateAndHour > NewTime.DateAndHour);

    public override void Validate()
    {
        if (TimesScheduled.Count < 2) return;
        CheckFirstConditionValid();
        CheckSecondConditionValid();
        CheckThirdConditionValid();
    }

    private void CheckFirstConditionValid()
    {
        BusinessException.ThrowIf(IsLessThan && !IsLessOrEqualThanNext, NewTimeBeforeNextTime);
    }

    private void CheckSecondConditionValid()
    {
        BusinessException.ThrowIf(IsGreaterThan && !IsBiggerOrEqualPrev, NewTimeAfterPrevTime);
    }

    private void CheckThirdConditionValid()
    {
        BusinessException.ThrowIf(IsNewTimeBetweenPrevNext && !IsAgendaWithinTimeBounds, NewTimeBetweenPrevNext);
    }

    private bool IsNewTimeBetweenPrevNext => IsGreaterThanPrev && IsLessThanNext;
    private bool IsAgendaWithinTimeBounds => IsLessOrEqualThanNext && IsPrevLessOrEqualNewTime;
    private bool IsLessOrEqualThanNext => NewTime.PlusDuration() <= NextTime?.DateAndHour;
    private bool IsPrevLessOrEqualNewTime => NewTime.DateAndHour >= PrevTime?.PlusDuration();
    private bool IsBiggerOrEqualPrev => NewTime.DateAndHour >= TimesScheduled.Last().PlusDuration();
    private bool IsGreaterThanPrev => NewTime.DateAndHour > PrevTime?.DateAndHour;
    private bool IsLessThanNext => NewTime.DateAndHour < NextTime?.DateAndHour;
}