namespace E7BeautyShop.Schedule;

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
        BusinessException.When(StartAt == TimeSpan.Zero, "StartAt cannot be empty");
        BusinessException.When(EndAt == TimeSpan.Zero, "EndAt cannot be empty");
        BusinessException.When(StartAt >= EndAt, "StartAt cannot be greater than EndAt");
    }
}