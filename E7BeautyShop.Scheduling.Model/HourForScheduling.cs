namespace E7BeautyShop.Domain;

public class HourForScheduling
{
    public TimeSpan HourWeekday { get; private set; }
    public TimeSpan HourWeekend { get; private set; }
    public TimeSpan HourHoliday { get; private set; }
    public bool IsAvailable { get; private set; }

    public HourForScheduling CreateHourWeekday(TimeSpan hourWeekday)
    {
        HourWeekday = hourWeekday;
        IsAvailable = true;
        return this;
    }
    
    public HourForScheduling CreateHourWeekend(TimeSpan hourWeekend)
    {
        HourWeekend = hourWeekend;
        IsAvailable = true;
        return this;
    }
    
    public HourForScheduling CreateHourHoliday(TimeSpan hourHoliday)
    {
        HourHoliday = hourHoliday;
        IsAvailable = true;
        return this;
    }
}