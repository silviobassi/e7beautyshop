namespace E7BeautyShop.Schedule;

public sealed class DayRest
{
    
    
    public DayRest(DayOfWeek dayOnWeek)
    {
        DayOnWeek = dayOnWeek;
    }

    public DayOfWeek DayOnWeek { get; private set; }
}