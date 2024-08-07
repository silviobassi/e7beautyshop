﻿using E7BeautyShop.AgendaService.Domain.Entities;

namespace E7BeautyShop.AgendaService.Domain.Services;

public abstract class AbstractValidatorOfficeHoursScheduled(IReadOnlyCollection<OfficeHour> timesScheduled)
{
    protected readonly IReadOnlyCollection<OfficeHour> TimesScheduled =
        timesScheduled ?? throw new ArgumentNullException(nameof(timesScheduled));
    
    public abstract void Validate();
    
}
