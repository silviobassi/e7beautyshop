namespace E7BeautyShop.Appointment.Core;

public abstract class WeekDayOrWeekend
{
    public TimeSpan StartAt { get; private set; }
    public TimeSpan EndAt { get; private set; }

    protected WeekDayOrWeekend(TimeSpan start, TimeSpan end)
    {
        StartAt = start;
        EndAt = end;
        Validate();
    }

    private void Validate()
    {
        BusinessNullException.When(StartAt == default, nameof(StartAt));
        BusinessNullException.When(EndAt == default, nameof(EndAt));
        BusinessException.When(StartAt >= EndAt, "StartAt cannot be greater than EndAt");
    }
}