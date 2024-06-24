namespace E7BeautyShop.AgendaService.Core.Entities;

public sealed class DayRest : Entity
{
    public DayOfWeek? DayOnWeek { get; private set; }

    public Guid ScheduleId { get; init; }

    public DayRest()
    {
    }

    private DayRest(DayOfWeek? dayOnWeek)
    {
        DayOnWeek = dayOnWeek;
        ArgumentNullException.ThrowIfNull(nameof(DayOnWeek));
    }

    public static DayRest Create(DayOfWeek? dayOnWeek) => new(dayOnWeek);
}