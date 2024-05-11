namespace E7BeautyShop.Domain;

public class OfficeDay(DateTime date, int interval, Weekday weekday, Weekend weekend, DayOfWeek dayRest)
{
    public DateTime Date { get; private set; } = date;
    public int Interval { get; private set; } = interval;
    public bool IsAttending { get; private set; } = true;
    public List<OfficeHour> OfficeHours { get; private set; } = [];

    public void GenerateWeekday()
    {
        if (IsNotWeekday() || IsDayRest()) return;
        AddAllHoursToWeekdays();
    }


    public void GenerateWeekend()
    {
        if (IsNotWeekend() || IsDayRest()) return;
        AddAllHoursToWeekend();
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
        return Date.DayOfWeek == dayRest;
    }

    private void AddAllHoursToWeekdays()
    {
        for (var currentTime = weekday.StartAt; currentTime <= weekday.EndAt; currentTime += TimeSpan.FromMinutes(Interval))
            OfficeHours.Add(new OfficeHour(currentTime));
    }
    
    private void AddAllHoursToWeekend()
    {
        for (var currentTime = weekend.StartAt; currentTime <= weekend.EndAt; currentTime += TimeSpan.FromMinutes(Interval))
            OfficeHours.Add(new OfficeHour(currentTime));
    }
}