﻿using E7BeautyShop.AgendaService.Core.Entities;

namespace E7BeautyShop.AgendaService.Core.Services;

public abstract class AbstractValidatorTimeToSchedule(
    IReadOnlyCollection<OfficeHour> officeHoursScheduled,
    OfficeHour timeToSchedule)
    : AbstractValidatorOfficeHoursScheduled(officeHoursScheduled.OrderBy(of => of.DateAndHour).ToList())
{
    protected readonly OfficeHour TimeToSchedule =
        timeToSchedule ?? throw new ArgumentNullException(nameof(timeToSchedule));

    protected bool IsTimeToScheduleLessThanCurrentTime =>
        TimeToSchedule.DateAndHour < OfficeHourScheduled.First().DateAndHour;
    
    protected bool IsTimeToScheduleGreaterThanCurrentTime =>
        TimeToSchedule.DateAndHour > OfficeHourScheduled.Last().DateAndHour;
}