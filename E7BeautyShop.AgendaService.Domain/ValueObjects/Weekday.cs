using E7BeautyShop.AgendaService.Domain.ObjectsValue;

namespace E7BeautyShop.AgendaService.Domain.ValueObjects;

public sealed record Weekday : WeekDayOrWeekend
{
    public Weekday()
    {
    }

    private Weekday(TimeSpan? startAt, TimeSpan? endAt) : base(startAt, endAt)
    {
    }

    public static implicit operator Weekday((TimeSpan startAt, TimeSpan endAt) tuple)
        => new(tuple.startAt, tuple.endAt);
}