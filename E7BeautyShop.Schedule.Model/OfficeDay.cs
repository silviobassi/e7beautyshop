using E7BeautyShop.Schedule;

namespace E7BeautyShop.Domain;

public class OfficeDay : Entity
{
    public DateTime? StartAt { get; set; }
    public bool IsAttending { get; set; } = true;
    public List<OfficeHour> OfficeHours { get; } = [];
    
    private DayOfWeek? DayRest { get; set; }
    private Weekday? Weekday { get; set; }
    private Weekend? Weekend { get; set; }
    
    public OfficeDay(DateTime startAt, Weekday? weekday, Weekend? weekend, DayOfWeek? dayRest)
    {
        Validate(weekday, weekend, dayRest);
        StartAt = startAt;
        Weekday = weekday;
        Weekend = weekend;
        DayRest = dayRest;
    }

    public void Update(Guid id, Weekday? weekday, Weekend? weekend, DayOfWeek? dayRest)
    {
        ModelBusinessException.When(id == Guid.Empty, "Id is required");
        Validate( weekday, weekend, dayRest);
        Id = id;
        Weekday = weekday;
        Weekend = weekend;
        DayRest = dayRest;
    }

    public void AddOfficeHour(OfficeHour officeHour)
    {
        AddWeekdayOfficeHours(officeHour);
        AddWeekendOfficeHours(officeHour);
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

    private static void Validate(Weekday? weekday, Weekend? weekend, DayOfWeek? dayRest)
    {
        ModelBusinessException.When(weekday == null, "Weekday is required");
        ModelBusinessException.When(weekend == null, "Weekend is required");
        ModelBusinessException.When(dayRest == null, "Day rest is required");
    }

    public bool IsNotWeekday => StartAt?.DayOfWeek is DayOfWeek.Saturday or DayOfWeek.Sunday;

    private bool IsNotWeekend => StartAt?.DayOfWeek is not DayOfWeek.Saturday && StartAt?.DayOfWeek is not DayOfWeek.Sunday;

    private bool IsDayRest => StartAt?.DayOfWeek == DayRest;


    private void AddWeekdayOfficeHours(OfficeHour officeHour)
    {
        if (IsNotWeekday || IsDayRest) return;
        AddOfficeHourOnWeekday(officeHour);
    }

    private void AddOfficeHourOnWeekday(OfficeHour officeHour)
    {
        officeHour.Hour ??= Weekday?.StartAt;
        if (officeHour.Hour > Weekday?.EndAt) return;
        OfficeHours.Add(officeHour);
    }

    private void AddWeekendOfficeHours(OfficeHour officeHour)
    {
        if (IsNotWeekend || IsDayRest) return;
        AddOfficeHourOnWeekend(officeHour);
    }

    private void AddOfficeHourOnWeekend(OfficeHour officeHour)
    {
        officeHour.Hour ??= Weekend?.StartAt;
        if (officeHour.Hour > Weekend?.EndAt) return;
        OfficeHours.Add(officeHour);
    }
}