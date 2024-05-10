namespace E7BeautyShop.Domain;

public class Schedule
{
    public DateOnly StartAt { get; private set; }
    public OfficeHours OfficeHours { get; private set; }

    public ISet<HourForScheduling> HoursForScheduling { get; private set; } = new HashSet<HourForScheduling>();

    public int ScheduleDurationInMonths { get; private set; }
    public int Interval { get; private set; }
    public IEnumerable<string>? DaysRest { get; private set; }


    public Schedule(DateOnly startAt, OfficeHours officeHours,
        int scheduleDurationInMonths,
        int interval,
        List<string> daysRest)
    {
        StartAt = startAt;
        OfficeHours = officeHours;
        ScheduleDurationInMonths = scheduleDurationInMonths;
        Interval = interval;
        DaysRest = daysRest;
    }

    public bool AddHoursForScheduling(HourForScheduling hourForScheduling)
    {
        return HoursForScheduling.Add(hourForScheduling);
    }

    public void Generate()
    {
        // Gerar horários para dias úteis
        
        var intervalTimeSpan = TimeSpan.FromMinutes(Interval);

        for (var currentTime = OfficeHours.StartAt;
             currentTime
             <= OfficeHours.EndAt;
             currentTime += intervalTimeSpan)
        {
            HoursForScheduling.Add(new HourForScheduling().CreateHourWeekday(currentTime));
        }
    }
}