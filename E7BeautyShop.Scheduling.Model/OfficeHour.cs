namespace E7BeautyShop.Domain;

public class OfficeHour : IOfficeHour
{
    public TimeSpan? Hour { get; set; }
    public bool IsAvailable { get; private set; } = true;
    public OfficeHour(TimeSpan? hour)
    {
        Validate(hour);
        Hour = hour;
    }
    

    public  void ToMakeAvailable()
    {
        IsAvailable = true;
    }

    public  void ToMakeUnavailable()
    {
        IsAvailable = false;
    }

    private static void Validate(TimeSpan? hour)
    {
        ModelBusinessException.When(hour == null, "Hour is required");
        ModelBusinessException.When(hour < TimeSpan.FromHours(0), "Hour must be greater than 0");
    }
}