namespace E7BeautyShop.Appointment.Core;

public sealed class Weekday : WeekDayOrWeekend
{
    public Weekday(TimeSpan startAt, TimeSpan endAt)
    {
        StartAt = startAt;
        EndAt = endAt;
        Validate();
    }
}