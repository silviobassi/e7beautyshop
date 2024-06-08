namespace E7BeautyShop.Appointment.Core;

public class DayRest : Entity
{
    public DayRest(DayOfWeek? dayOnWeek)
    {
        DayOnWeek = dayOnWeek;
        BusinessNullException.When(dayOnWeek is null, nameof(DayOnWeek));
    }

    public DayOfWeek? DayOnWeek { get; init; }
    public Guid? ScheduleId { get; init; }
}