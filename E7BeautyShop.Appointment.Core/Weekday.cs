﻿namespace E7BeautyShop.Appointment.Core;

public sealed class Weekday : WeekDayOrWeekend
{
    public Weekday()
    {
    }

    public Weekday(TimeSpan startAt, TimeSpan endAt) : base(startAt, endAt)
    {
    }

    public static implicit operator Weekday((TimeSpan startAt, TimeSpan endAt) tuple)
        => new(tuple.startAt, tuple.endAt);
}