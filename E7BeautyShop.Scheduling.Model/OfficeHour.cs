namespace E7BeautyShop.Domain;

public class OfficeHour
{
    public TimeSpan Hour { get; private set; }
    public bool IsAvailable { get; private set; }

    public OfficeHour(TimeSpan hour)
    {
        Hour = hour;
        IsAvailable = true;
    }
}