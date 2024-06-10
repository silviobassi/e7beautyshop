namespace E7BeautyShop.Appointment.Core;

public sealed class Weekend : WeekDayOrWeekend
{
    public Weekend()
    {
    }

    private Weekend(TimeSpan startAt, TimeSpan endAt) : base(startAt, endAt)
    {
    }

    public static implicit operator Weekend((TimeSpan startAt, TimeSpan endAt) tuple)
        => new(tuple.startAt, tuple.endAt);
}