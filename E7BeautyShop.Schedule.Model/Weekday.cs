namespace E7BeautyShop.Schedule;

public sealed class Weekday: CheckDaysOfWeek
{
    
    public TimeSpan StartAt { get; private set; }
    public TimeSpan EndAt { get; private set; }
    public Weekday(TimeSpan start, TimeSpan end)
    {
        Validate(start, end);
        StartAt = start;
        EndAt = end;
    }
}