namespace E7BeautyShop.Domain;

public class OfficeHour
{
    public TimeSpan Hour { get; private set; }
    public bool IsAvailable { get; private set; } = true;

    public OfficeHour(TimeSpan hour)
    {
        Hour = hour;
    }
    
    public void ToMakeAvailable()
    {
        IsAvailable = true;
    }
    
    public void ToMakeUnavailable()
    {
        IsAvailable = false;
    }
}