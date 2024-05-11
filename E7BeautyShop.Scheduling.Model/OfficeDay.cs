namespace E7BeautyShop.Domain;

public class OfficeDay : Entity
{
    public DateTime? Date { get; private set; }
    public bool IsAttending { get; private set; } = true;
    public List<OfficeHour> OfficeHours { get; private set; } = [];

    private readonly Weekday? _weekday;
    private readonly Weekend? _weekend;
    private readonly DayOfWeek? _dayRest;
    private readonly int _interval;

    public OfficeDay(int interval, Weekday? weekday, Weekend? weekend, DayOfWeek? dayRest)
    {
        ModelBusinessException.When(interval <= 0, "Interval is required");
        ModelBusinessException.When(weekday == null, "Weekday is required");
        ModelBusinessException.When(weekend == null, "Weekend is required");
        ModelBusinessException.When(dayRest == null, "Day rest is required");
        _interval = interval;
        _weekday = weekday;
        _weekend = weekend;
        _dayRest = dayRest;
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
        return Date?.DayOfWeek == _dayRest;
    }

    private void GenerateWeekday()
    {
        if (IsNotWeekday() || IsDayRest()) return;
        AddAllWeekday();
    }

    private void AddAllWeekday()
    {
        for (var currentTime = _weekday?.StartAt;
             currentTime <= _weekday?.EndAt;
             currentTime += TimeSpan.FromMinutes(_interval))
            OfficeHours.Add(new OfficeHour(currentTime));
    }

    private void GenerateWeekend()
    {
        if (IsNotWeekend() || IsDayRest()) return;
        AddAllWeekend();
    }

    private void AddAllWeekend()
    {
        for (var currentTime = _weekend?.StartAt;
             currentTime <= _weekend?.EndAt;
             currentTime += TimeSpan.FromMinutes(_interval))
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