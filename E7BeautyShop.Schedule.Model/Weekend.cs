namespace E7BeautyShop.Schedule;

public class Weekend : CheckDaysOfWeek
{
    public TimeSpan StartAt { get; private set; }
    public TimeSpan EndAt { get; private set; }

    public Weekend(TimeSpan start, TimeSpan end)
    {
        Validate(start, end);
        StartAt = start;
        EndAt = end;
    }
}