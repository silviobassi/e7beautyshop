﻿using E7BeautyShop.AgendaService.Core.Entities;
using E7BeautyShop.AgendaService.Core.Validations;
using static E7BeautyShop.AgendaService.Core.Validations.Messages;

namespace E7BeautyShop.AgendaService.Core.Services;

public sealed class HasUniqueItemValid(IReadOnlyCollection<OfficeHour> timesScheduled, OfficeHour newTime)
    : AbstractValidatorTimeToSchedule(timesScheduled, newTime)
{
    public override bool Validate()
    {
        if (TimesScheduled.Count != 1) return false;
        BusinessException.ThrowIf(IsLessThan && !IsLessOrEqual, NewTimeBefore);
        BusinessException.ThrowIf(IsGreaterThan && !IsBiggerOrEqual, NewTimeAfter);
        return false;
    }

    private bool IsLessOrEqual => NewTime.PlusDuration() <= TimesScheduled.First().DateAndHour;

    private bool IsBiggerOrEqual => NewTime.DateAndHour >= TimesScheduled.First().PlusDuration();
}