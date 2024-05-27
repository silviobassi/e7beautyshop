namespace E7BeautyShop.Schedule;

public sealed class DayRest(DayOfWeek dayOnWeek)
{
    public DayOfWeek DayOnWeek { get; private set; } = dayOnWeek;
}