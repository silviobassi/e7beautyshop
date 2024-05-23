namespace E7BeautyShop.Schedule;

public class OfficeDay
{
    public DateTime DateTime { get; private set; }
    public List<OfficeHour> TimesOfDay { get; private set; } = [];

    public OfficeDay(DateTime dateTime)
    {
        DateTime = dateTime;
    }
    public void AddOfficeHour(OfficeHour timeOfDay)
    {
        TimesOfDay.Add(timeOfDay);
    }
}