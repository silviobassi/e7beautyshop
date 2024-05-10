namespace E7BeautyShop.Domain;

public class Schedule
{
    public DateOnly StartAt { get; private set; }
    public OfficeHoursOnWeekday OfficeHoursOnWeekday { get; private set; }
    public OfficeHoursOnHoliday OfficeHoursOnHoliday { get; private set; }
    public OfficeHoursOnWeekend OfficeHoursOnWeekend { get; private set; }

    public ISet<HourForScheduling> HoursForScheduling { get; private set; } = new HashSet<HourForScheduling>();

    public int ScheduleDurationInMonths { get; private set; }
    public int Interval { get; private set; }
    public IEnumerable<string>? DiasDeDescanso { get; private set; }


    public Schedule(DateOnly startAt, OfficeHoursOnWeekday officeHoursOnWeekday,
        OfficeHoursOnHoliday officeHoursOnHoliday,
        OfficeHoursOnWeekend officeHoursOnWeekend,
        int scheduleDurationInMonths,
        int interval,
        List<string> diasDeDescanso)
    {
        StartAt = startAt;
        OfficeHoursOnWeekday = officeHoursOnWeekday;
        OfficeHoursOnHoliday = officeHoursOnHoliday;
        OfficeHoursOnWeekend = officeHoursOnWeekend;
        ScheduleDurationInMonths = scheduleDurationInMonths;
        Interval = interval;
        DiasDeDescanso = diasDeDescanso;
    }

    public bool AddHoursForScheduling(HourForScheduling hourForScheduling)
    {
        return HoursForScheduling.Add(hourForScheduling);
    }

    public void Generate()
    {
        var intervalTimeSpan = TimeSpan.FromMinutes(Interval);

        for (var currentTime = OfficeHoursOnWeekday.StartAt;
             currentTime
             <= OfficeHoursOnWeekday.EndAt;
             currentTime += intervalTimeSpan)
        {
            HoursForScheduling.Add(new HourForScheduling(currentTime));
        }
    }
}