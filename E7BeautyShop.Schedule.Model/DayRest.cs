namespace E7BeautyShop.Schedule;

public struct DayRest(DayOfWeek dayOnWeek)
{
    public DayOfWeek DayOnWeek { get; private set; } = dayOnWeek;
}