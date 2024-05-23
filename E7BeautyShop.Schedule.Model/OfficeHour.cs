namespace E7BeautyShop.Schedule;

public class OfficeHour
{
    
    public TimeSpan TimeOfDay { get; private set; }
    public OfficeHour(TimeSpan timeOfDay)
    {
        TimeOfDay = timeOfDay;
    }
}