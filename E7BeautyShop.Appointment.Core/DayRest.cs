namespace E7BeautyShop.Appointment.Core;

public readonly struct DayRest
{
    public DayRest(DayOfWeek? dayOnWeek)
    {
        DayOnWeek = dayOnWeek;
        BusinessNullException.When(dayOnWeek is null, nameof(DayOnWeek));
    }

    public DayOfWeek? DayOnWeek { get; }
}