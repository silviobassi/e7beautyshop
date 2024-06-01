namespace E7BeautyShop.Schedule;

public readonly struct DayRest(DayOfWeek dayOnWeek)
{
    public DayOfWeek DayOnWeek { get; } = dayOnWeek;
}