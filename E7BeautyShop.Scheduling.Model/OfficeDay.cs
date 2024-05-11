namespace E7BeautyShop.Domain;

public class OfficeDay : Entity
{
    public DateTime? Date { get; private set; }
    public bool IsAttending { get; private set; } = true;
    public List<OfficeHour> OfficeHours { get; private set; } = [];

    private Weekday? Weekday { get; set; }
    private Weekend? Weekend { get; set; }
    private DayOfWeek? DayRest { get; set; }
    private int Interval { get; set; }

    public OfficeDay(int interval, Weekday? weekday, Weekend? weekend, DayOfWeek? dayRest)
    {
        Validate(interval, weekday, weekend, dayRest);
        Interval = interval;
        Weekday = weekday;
        Weekend = weekend;
        DayRest = dayRest;
    }
    
    public void Update(Guid id, int interval, Weekday? weekday, Weekend? weekend, DayOfWeek? dayRest)
    {
        ModelBusinessException.When(id == Guid.Empty, "Id is required");
        Validate(interval, weekday, weekend, dayRest);
        Id = id;
        Interval = interval;
        Weekday = weekday;
        Weekend = weekend;
        DayRest = dayRest;
    }

    public void Generate()
    {
        GenerateWeekday();
        GenerateWeekend();
    }

    public void Cancel()
    {
        ModelBusinessException.When(!IsAttending, "Day is already canceled");
        IsAttending = false;
    }

    public void Attend()
    {
        ModelBusinessException.When(IsAttending, "Day is already attending");
        IsAttending = true;
    }

    private static void Validate(int interval, Weekday? weekday, Weekend? weekend, DayOfWeek? dayRest)
    {
        ModelBusinessException.When(interval <= 0, "Interval is required");
        ModelBusinessException.When(weekday == null, "Weekday is required");
        ModelBusinessException.When(weekend == null, "Weekend is required");
        ModelBusinessException.When(dayRest == null, "Day rest is required");
    }

    private bool IsNotWeekday()
    {
        return Date?.DayOfWeek is DayOfWeek.Saturday or DayOfWeek.Sunday;
    }

    private bool IsNotWeekend()
    {
        return Date?.DayOfWeek is not DayOfWeek.Saturday && Date?.DayOfWeek is not DayOfWeek.Sunday;
    }

    private bool IsDayRest()
    {
        return Date?.DayOfWeek == DayRest;
    }

    private void GenerateWeekday()
    {
        if (IsNotWeekday() || IsDayRest()) return;
        AddAllWeekday();
    }

    private void AddAllWeekday()
    {
        for (var currentTime = Weekday?.StartAt;
             currentTime <= Weekday?.EndAt;
             currentTime += TimeSpan.FromMinutes(Interval))
            OfficeHours.Add(new OfficeHour(currentTime));
    }

    private void GenerateWeekend()
    {
        if (IsNotWeekend() || IsDayRest()) return;
        AddAllWeekend();
    }

    private void AddAllWeekend()
    {
        for (var currentTime = Weekend?.StartAt;
             currentTime <= Weekend?.EndAt;
             currentTime += TimeSpan.FromMinutes(Interval))
            OfficeHours.Add(new OfficeHour(currentTime));
    }

    public void InformDate(DateTime? date)
    {
        ModelBusinessException.When(date == null, "Date is required");
        Date = date;
    }

    internal void IncrementDate(int daysNumber)
    {
        ModelBusinessException.When(daysNumber < 0, "Days number must be greater than 0");
        Date = Date?.AddDays(daysNumber);
    }
}