﻿using E7BeautyShop.Appointment.Core.Entities;

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

        if (TimeToScheduleLessThanCurrentTime)
            return TimeToSchedulePlusDurationLessThanNextTime;

        if (TimeToScheduleGreaterThanCurrentTime)
            return TimeToScheduleGreaterThanPrevTimePlusDuration;

        return TimeToScheduleGreaterThanPrevTime && TimeToScheduleLessThanNextTime &&
               (TimeToSchedulePlusDurationLessThanNextTime && PrevTimePlusDurationLessThanTimeToSchedule);
    }

    private bool TimeToScheduleLessThanCurrentTime =>
        TimeToSchedule.DateAndHour < OfficeHourScheduled.First().DateAndHour;

    private bool TimeToSchedulePlusDurationLessThanNextTime => TimeToSchedule.PlusDuration() <= NextTime?.DateAndHour;

    private bool TimeToScheduleGreaterThanPrevTimePlusDuration =>
        TimeToSchedule.DateAndHour >= OfficeHourScheduled.Last().PlusDuration();

    private bool TimeToScheduleGreaterThanCurrentTime =>
        TimeToSchedule.DateAndHour > OfficeHourScheduled.Last().DateAndHour;

    private bool TimeToScheduleGreaterThanPrevTime => TimeToSchedule.DateAndHour > PrevTime?.DateAndHour;

    private bool TimeToScheduleLessThanNextTime => TimeToSchedule.DateAndHour < NextTime?.DateAndHour;
    private bool PrevTimePlusDurationLessThanTimeToSchedule => PrevTime?.PlusDuration() <= TimeToSchedule.DateAndHour;
}