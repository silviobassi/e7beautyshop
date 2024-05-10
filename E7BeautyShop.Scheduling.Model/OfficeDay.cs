using E7BeautyShop.Domain;

public class OfficeDay
{
    public DateTime Date { get; private set; }
    public int Interval { get; private set; }
    public DayOfWeek DayRest { get; private set; }
    public bool IsAttending { get; private set; } = true;
    public List<OfficeHour> OfficeHours { get; private set; } = [];

    private readonly Weekday _weekday;
    private readonly Weekend _weekend;

    public OfficeDay(DateTime date, int interval, TimeSpan startWeekDay, TimeSpan endWeekDay, TimeSpan startWeekend, TimeSpan endWeekend, DayOfWeek dayRest)
    {
        Date = date;
        Interval = interval;
        DayRest = dayRest;
        _weekday = new Weekday(startWeekDay, endWeekDay);
        _weekend = new Weekend(startWeekend, endWeekend);
    }

    public void GenerateWeekday()
    {
        var intervalTimeSpan = TimeSpan.FromMinutes(Interval);
        if (IsNotWeekday() || IsDayRest()) return;
        for (var currentTime = _weekday.StartAt; currentTime <= _weekday.EndAt; currentTime += intervalTimeSpan)
        {
            OfficeHours.Add(new OfficeHour(currentTime));
        }
    }

    public void GenerateWeekend()
    {
        var intervalTimeSpan = TimeSpan.FromMinutes(Interval);
        if (IsNotWeekend() || IsDayRest()) return;
        for (var currentTime = _weekend.StartAt; currentTime <= _weekend.EndAt; currentTime += intervalTimeSpan)
        {
            OfficeHours.Add(new OfficeHour(currentTime));
        }
    }

    public void Cancel()
    {
        IsAttending = false;
    }
    
    public void Attend()
    {
        IsAttending = true;
    }

    private bool IsNotWeekday()
    {
        return Date.DayOfWeek is DayOfWeek.Saturday or DayOfWeek.Sunday;
    }

    private bool IsNotWeekend()
    {
        return Date.DayOfWeek is not DayOfWeek.Saturday && Date.DayOfWeek is not DayOfWeek.Sunday;
    }
    
    private bool IsDayRest()
    {
        return Date.DayOfWeek == DayRest;
    }
}