namespace E7BeautyShop.Schedule;

public sealed class Schedule
{
    public DateTime StartAt { get; private set; }
    public DateTime EndAt { get; private set; }

    public TimeSpan StartWeekday { get; private set; }
    public TimeSpan EndWeekday { get; private set; }
    public TimeSpan StartWeekend { get; private set; }
    public TimeSpan EndWeekend { get; private set; }
    public List<DayOfWeek> DayRest { get; private set; }
    public List<OfficeDay> OfficeDays { get; private set; }
    public Guid ProfessionalId { get; private set; }
    public Guid CustomerId { get; private set; }


    public Schedule(
        DateTime startAt,
        DateTime endAt,
        TimeSpan startWeekday,
        TimeSpan endWeekday,
        TimeSpan startWeekend,
        TimeSpan endWeekend,
        List<DayOfWeek> dayRest,
        List<OfficeDay> officeDays,
        Guid professionalId,
        Guid customerId)
    {
        StartAt = startAt;
        EndAt = endAt;
        StartWeekday = startWeekday;
        EndWeekday = endWeekday;
        StartWeekend = startWeekend;
        EndWeekend = endWeekend;
        DayRest = dayRest;
        OfficeDays = officeDays;
        ProfessionalId = professionalId;
        CustomerId = customerId;
    }

    public void AddOfficeDay(OfficeDay day) => OfficeDays.Add(day);
    
}