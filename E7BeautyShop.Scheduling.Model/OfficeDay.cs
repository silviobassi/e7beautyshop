using E7BeautyShop.Domain;

public class OfficeDay
{
    public DateTime Date { get; private set; }
    public int Interval { get; private set; }

    private readonly TimeSpan _startWeekDay;
    private readonly TimeSpan _endWeekDay;

    private readonly TimeSpan _startWeekend;
    private readonly TimeSpan _endWeekend;

    public List<OfficeHour> OfficeHours { get; private set; } = [];

    public OfficeDay(DateTime date, int interval, TimeSpan startWeekDay, TimeSpan endWeekDay, TimeSpan startWeekend, TimeSpan endWeekend)
    {
        Date = date;
        Interval = interval;
        _startWeekDay = startWeekDay;
        _endWeekDay = endWeekDay;
        _startWeekend = startWeekend;
        _endWeekend = endWeekend;
    }

    public void GenerateWeekday()
    {
        var intervalTimeSpan = TimeSpan.FromMinutes(Interval);
        if (IsNotWeekday()) return;
        for (var currentTime = _startWeekDay; currentTime <= _endWeekDay; currentTime += intervalTimeSpan)
        {
            OfficeHours.Add(new OfficeHour(currentTime));
        }
    }

    public void GenerateWeekend()
    {
        var intervalTimeSpan = TimeSpan.FromMinutes(Interval);
        if (IsNotWeekend()) return;
        for (var currentTime = _startWeekend; currentTime <= _endWeekend; currentTime += intervalTimeSpan)
        {
            OfficeHours.Add(new OfficeHour(currentTime));
        }
    }

    private bool IsNotWeekday()
    {
        return Date.DayOfWeek is DayOfWeek.Saturday or DayOfWeek.Sunday;
    }

    private bool IsNotWeekend()
    {
        return Date.DayOfWeek is not DayOfWeek.Saturday && Date.DayOfWeek is not DayOfWeek.Sunday;
    }
}