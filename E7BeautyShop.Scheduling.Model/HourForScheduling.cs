namespace E7BeautyShop.Domain;

public class HourForScheduling
{
    public TimeSpan Hour { get; private set; }
    public bool IsAvailable { get; private set; }

    public HourForScheduling(TimeSpan hour)
    {
        Hour = hour;
        IsAvailable = true;
    }
}