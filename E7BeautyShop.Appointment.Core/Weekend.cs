namespace E7BeautyShop.Appointment.Core;

public sealed class Weekend : WeekDayOrWeekend
{
    public Weekend(TimeSpan startAt, TimeSpan endAt)
    {
        StartAt = startAt;
        EndAt = endAt;
        Validate();
    }
}