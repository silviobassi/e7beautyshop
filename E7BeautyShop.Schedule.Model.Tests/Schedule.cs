namespace E7BeautyShop.Schedule.Tests;

internal class Schedule
{

    public DateTime? StartAt { get; private set; }
    public DayOfWeek DayRest { get; private set; }
    public Weekday Weekday { get; private set; }
    public Weekend Weekend { get; private set; }
    
    public List<OfficeDay> OfficeDays { get; } = [];
    
    public Schedule(DateTime startAt, DayOfWeek dayRest, Weekday weekday, Weekend weekend)
    {
        StartAt = startAt;
        DayRest = dayRest;
        Weekday = weekday;
        Weekend = weekend;
    }
    
    public bool IsNotWeekday => StartAt?.DayOfWeek is DayOfWeek.Saturday or DayOfWeek.Sunday;
    
    private bool IsNotWeekend =>
        StartAt?.DayOfWeek is not DayOfWeek.Saturday && StartAt?.DayOfWeek is not DayOfWeek.Sunday;
    
    private bool IsDayRest => StartAt?.DayOfWeek == DayRest;

    public void AddOfficeDay(OfficeDay officeDay)
    {
        OfficeDays.Add(officeDay);
    }
}
