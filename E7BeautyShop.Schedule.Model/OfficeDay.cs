namespace E7BeautyShop.Schedule;

public sealed class OfficeDay : Appointment
{
    public DateTime DateTime { get; private set; }
    public List<OfficeHour> TimesOfDay { get; } = [];

    public OfficeDay(DateTime dateTime)
    {
        DateTime = dateTime;
        IsAvailable = true;
        Validate();
    }

    public void AddOfficeHour(OfficeHour timeOfDay)
    {
        TimesOfDay.Add(timeOfDay);
    }

    private void Validate() 
        => BusinessException.When(DateTime == DateTime.MinValue, "DateTime cannot be empty");
}