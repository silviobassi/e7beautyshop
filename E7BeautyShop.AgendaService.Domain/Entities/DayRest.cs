namespace E7BeautyShop.AgendaService.Domain.Entities;

public sealed class DayRest : Entity
{
    public DayOfWeek? DayOnWeek { get; private set; }

    public Guid AgendaId { get; init; }

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