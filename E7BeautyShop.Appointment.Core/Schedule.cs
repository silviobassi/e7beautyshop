namespace E7BeautyShop.Appointment.Core;

public sealed class Schedule : Entity, IAggregateRoot
{
    
    public Schedule(){}
    public Schedule(DateTime startAt, DateTime endAt, Professional professional, Weekday weekday,
        Weekend weekend)
    {
        Id = Guid.NewGuid();
        StartAt = startAt;
        EndAt = endAt;
        Professional = professional;
        Weekday = weekday;
        Weekend = weekend;
        Validate();
    }
    

    public DateTime StartAt { get; private set; }

    public DateTime EndAt { get; private set; }

    public Professional? Professional { get; private set; }

    public Weekday? Weekday { get; private set; }
    public Weekend? Weekend { get; private set; }

    public List<DayRest> DaysRest { get; } = [];

    public List<OfficeHour> OfficeHours { get; } = [];

    public void AddDayRest(DayRest dayRest) => DaysRest.Add(dayRest);

    public void AddOfficeHour(OfficeHour officeHour)
    {
        if (IsDayRest(officeHour)) return;
        OfficeHours.Add(officeHour);
    }

    private bool IsDayRest(OfficeHour officeHour)
    {
        var existsDayRest = DaysRest.Exists(dr => dr.DayOnWeek == officeHour.DateAndHour.DayOfWeek);
        return existsDayRest && DaysRest.Count > 0;
    }

    public static bool IsWeekday(OfficeHour officeHour) => !IsWeekend(officeHour);

    private static bool IsWeekend(OfficeHour? officeHour)
        => officeHour?.DateAndHour.DayOfWeek is DayOfWeek.Sunday or DayOfWeek.Saturday;


    private void Validate()
    {
        BusinessException.When(StartAt == DateTime.MinValue, "StartAt cannot be empty");
        BusinessException.When(EndAt == DateTime.MinValue, "EndAt cannot be empty");
        BusinessNullException.When(Professional is null, nameof(Professional));
        BusinessNullException.When(Weekday is null, nameof(Weekday));
        BusinessNullException.When(Weekend is null, nameof(Weekend));
    }
}