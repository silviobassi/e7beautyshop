namespace E7BeautyShop.Schedule;

public sealed class OfficeDay
{
    public DateTime DateTime { get; private set; }
    public List<OfficeHour> TimesOfDay { get; } = [];

    public OfficeDay(DateTime dateTime)
    {
        BusinessException.When(dateTime == DateTime.MinValue, "DateTime cannot be empty");
        DateTime = dateTime;
    }

    public void AddOfficeHour(OfficeHour timeOfDay)
    {
        TimesOfDay.Add(timeOfDay);
    }
}