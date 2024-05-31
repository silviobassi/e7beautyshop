namespace E7BeautyShop.Schedule;

public sealed class Schedule : IAggregateRoot
{
    private readonly Weekday _weekday;
    private readonly Weekend _weekend;

    public Schedule(DateTime startAt, DateTime endAt, ProfessionalId professionalId, Weekday weekday, Weekend weekend)
    {
        StartAt = startAt;
        EndAt = endAt;
        ProfessionalId = professionalId;
        _weekday = weekday;
        _weekend = weekend;
        Validate();
    }

    public DateTime StartAt { get; private set; }

    public DateTime EndAt { get; private set; }

    public ProfessionalId ProfessionalId { get; private set; }
    public List<DayRest> DaysRest { get; } = [];

    public List<OfficeHour> OfficeHour { get; } = [];

    public void AddDayRest(DayRest dayRest) => DaysRest.Add(dayRest);

    public void AddDaysRest(List<DayRest> daysRest) => DaysRest.AddRange(daysRest);

    public void AddOfficeHour(OfficeHour officeHour)
    {
        if (IsDayRest(officeHour)) return;
        OfficeHour.Add(officeHour);
    }

    private static bool IsWeekday(OfficeHour officeHour) => !IsWeekend(officeHour);

    private bool IsDayRest(OfficeHour officeHour)
    {
        var existsDayRest = DaysRest.Exists(dr => dr.DayOnWeek == officeHour.ReserveDateAndHour.DayOfWeek);
        return existsDayRest && DaysRest.Count > 0;
    }

    private static bool IsWeekend(OfficeHour? officeHour)
    {
        if (officeHour?.ReserveDateAndHour.DayOfWeek == DayOfWeek.Saturday) return true;
        return officeHour?.ReserveDateAndHour.DayOfWeek == DayOfWeek.Sunday;
    }

    private void Validate()
    {
        BusinessException.When(StartAt == DateTime.MinValue, "StartAt cannot be empty");
        BusinessException.When(EndAt == DateTime.MinValue, "EndAt cannot be empty");
    }
}