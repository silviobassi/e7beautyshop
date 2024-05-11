namespace E7BeautyShop.Domain;

public class OfficeHour
{
    public TimeSpan? Hour { get; private set; }
    public bool IsAvailable { get; private set; } = true;

    public OfficeHour(TimeSpan? hour)
    {
        ModelBusinessException.When(hour == null, "Hour is required");
        ModelBusinessException.When(hour < TimeSpan.FromHours(0), "Hour must be greater than 0");
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