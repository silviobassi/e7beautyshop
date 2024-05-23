namespace E7BeautyShop.Schedule;

public sealed class Schedule
{
    public DateTime StartAt { get; private set; }
    public DateTime EndAt { get; private set; }

    public Weekday Weekday { get; private set; }
    public Weekend Weekend { get; private set; }
    public List<DayOfWeek> DayRest { get; private set; }
    public List<OfficeDay> OfficeDays { get; private set; }
    public Guid ProfessionalId { get; private set; }
    public Guid CustomerId { get; private set; }


    public Schedule(
        DateTime startAt,
        DateTime endAt,
        Weekday weekday,
        Weekend weekend,
        List<DayOfWeek> dayRest,
        List<OfficeDay> officeDays,
        Guid professionalId,
        Guid customerId)
    {
        StartAt = startAt;
        EndAt = endAt;
        Weekday = weekday;
        Weekend = weekend;
        DayRest = dayRest;
        OfficeDays = officeDays;
        ProfessionalId = professionalId;
        CustomerId = customerId;
    }

    public void AddOfficeDay(OfficeDay day) => OfficeDays.Add(day);
}