namespace E7BeautyShop.Appointment.Core;

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
        BusinessNullException.When(dayOnWeek is null, nameof(DayOnWeek));
    }

    public static DayRest Create(DayOfWeek? dayOnWeek) => new(dayOnWeek);
}