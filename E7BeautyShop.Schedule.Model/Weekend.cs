namespace E7BeautyShop.Schedule;

public class Weekend
{
    public TimeSpan StartAt { get; private set; }
    public TimeSpan EndAt { get; private set; }

    public Weekend(TimeSpan start, TimeSpan end)
    {
        StartAt = start;
        EndAt = end;
    }
}