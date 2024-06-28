using E7BeautyShop.AgendaService.Core.Entities;
using E7BeautyShop.AgendaService.Core.Validations;
using static E7BeautyShop.AgendaService.Core.Validations.Messages;

namespace E7BeautyShop.AgendaService.Core.Services;

/// <summary>
/// This class validates if there are at least two items in the schedule.
/// </summary>
public sealed class HasAtLeastTwoItemsValid(IReadOnlyCollection<OfficeHour> timesScheduled, OfficeHour newTime)
    : AbstractValidatorTimeToSchedule(timesScheduled, newTime)
{
    /// <summary>
    /// Validates the schedule.
    /// </summary>
    public override void Validate()
    {
        if (TimesScheduled.Count < 2) return;
        CheckFirstConditionValid();
        CheckSecondConditionValid();
        CheckThirdConditionValid();
    }

    /// <summary>
    /// Checks if the new time is less than or equal to next time.
    /// </summary>
    private void CheckFirstConditionValid()
    {
        BusinessException.ThrowIf(IsLessThan && !IsLessOrEqualToNext, NewTimeBeforeNext);
    }

    /// <summary>
    /// Checks if the new time is greater than time scheduled and bigger or equal to previous time.
    /// </summary>
    private void CheckSecondConditionValid()
    {
        BusinessException.ThrowIf(IsGreaterThan && !IsBiggerOrEqualPrev, NewTimeAfterPrev);
    }

    /// <summary>
    /// Checks if the new time is between the previous and next times.
    /// </summary>
    private void CheckThirdConditionValid()
    {
        BusinessException.ThrowIf(IsNewTimeBetweenPrevNext && !IsAgendaWithinTimeBounds, NewTimeBetweenPrevNext);
    }

    private bool IsNewTimeBetweenPrevNext => IsGreaterThanPrev && IsLessThanNext;
    private bool IsAgendaWithinTimeBounds => IsLessOrEqualToNext && IsPrevLessOrEqualNewTime;
    private bool IsLessOrEqualToNext => NewTime.PlusDuration() <= NextTime?.DateAndHour;
    private bool IsPrevLessOrEqualNewTime => NewTime.DateAndHour >= PrevTime?.PlusDuration();
    private bool IsBiggerOrEqualPrev => NewTime.DateAndHour >= TimesScheduled.Last().PlusDuration();
    private bool IsGreaterThanPrev => NewTime.DateAndHour > PrevTime?.DateAndHour;
    private bool IsLessThanNext => NewTime.DateAndHour < NextTime?.DateAndHour;

    /// <summary>
    /// Gets the previous time in the schedule.
    /// </summary>
    private OfficeHour? PrevTime => TimesScheduled.LastOrDefault(of => of.DateAndHour < NewTime.DateAndHour);

    /// <summary>
    /// Gets the next time in the schedule.
    /// </summary>
    private OfficeHour? NextTime => TimesScheduled.FirstOrDefault(of => of.DateAndHour > NewTime.DateAndHour);
}