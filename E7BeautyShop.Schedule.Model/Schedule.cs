namespace E7BeautyShop.Schedule;

public sealed class Schedule
{
    private readonly Weekday _weekday;
    private readonly Weekend _weekend;

    public Schedule(DateTime startAt, DateTime endAt, Weekday weekday, Weekend weekend)
    {
        BusinessException.When(startAt == DateTime.MinValue, "StartAt cannot be empty");
        BusinessException.When(endAt == DateTime.MinValue, "EndAt cannot be empty");
        StartAt = startAt;
        EndAt = endAt;
        _weekday = weekday;
        _weekend = weekend;
    }
    public DateTime StartAt { get; private set; }
    
    public DateTime EndAt { get; private set; }

    public List<DayRest> DaysRest { get; } = [];
    
    public List<OfficeDay> OfficeDays { get; } = [];

    public void AddDayRest(DayRest dayRest)=> DaysRest.Add(dayRest);
    
    public void AddDaysRest(List<DayRest> daysRest) => DaysRest.AddRange(daysRest);

    public void AddOfficeDay(OfficeDay day)
    {
        if (IsDayRest(day)) return;
        OfficeDays.Add(day);
    }

    internal (TimeSpan, TimeSpan) GetOfficeHours(OfficeDay officeDay) =>
        IsWeekday(officeDay) ? (_weekday.StartAt, _weekday.EndAt) : (_weekend.StartAt, _weekend.EndAt);

    public static bool IsWeekday(OfficeDay officeDay) => !IsWeekend(officeDay);

    private bool IsDayRest(OfficeDay officeDay)
    {
        var existsDayRest = DaysRest.Exists(dr => dr.DayOnWeek == officeDay.DateTime.DayOfWeek);
        return existsDayRest && DaysRest.Count > 0;
    }

    private static bool IsWeekend(OfficeDay? officeDay)
    {
        if (officeDay?.DateTime.DayOfWeek == DayOfWeek.Saturday) return true;
        return officeDay?.DateTime.DayOfWeek == DayOfWeek.Sunday;
    }
}