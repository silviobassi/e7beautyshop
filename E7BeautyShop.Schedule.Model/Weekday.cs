namespace E7BeautyShop.Schedule;

public class Weekday
{
    
    public TimeSpan StartAt { get; private set; }
    public TimeSpan EndAt { get; private set; }
    public Weekday(TimeSpan start, TimeSpan end)
    {
        StartAt = start;
        EndAt = end;
    }
}