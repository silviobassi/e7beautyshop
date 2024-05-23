namespace E7BeautyShop.Schedule;

public sealed class DayRest
{
    
    public DayOfWeek DayOnWeek { get; private set; }
    public DayRest(DayOfWeek dayOnWeek)
    {
        DayOnWeek = dayOnWeek;
    }
}