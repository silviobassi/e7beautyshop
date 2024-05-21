namespace E7BeautyShop.Schedule;

public class OfficeHour
{
    public TimeSpan? Hour { get; set; }
    public bool IsAvailable { get; private set; } = true;
    
    public void ToMakeAvailable()
    {
        IsAvailable = true;
    }

    public void ToMakeUnavailable()
    {
        IsAvailable = false;
    }
    
}