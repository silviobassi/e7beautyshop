namespace E7BeautyShop.Appointment.Core;

public class WeekDayOrWeekend
{
    public TimeSpan StartAt { get; protected set; }
    public TimeSpan EndAt { get; protected set; }
    
    public WeekDayOrWeekend()
    {
    }
    protected WeekDayOrWeekend(TimeSpan startAt, TimeSpan endAt)
    {
        StartAt = startAt;
        EndAt = endAt;
        Validate();
    }

    private void Validate()
    {
        BusinessNullException.When(StartAt == default, nameof(StartAt));
        BusinessNullException.When(EndAt == default, nameof(EndAt));
        BusinessException.When(StartAt >= EndAt, "StartAt cannot be greater than EndAt");
    }
}