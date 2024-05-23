namespace E7BeautyShop.Schedule;

public sealed class OfficeHour : Appointment
{
    
    public TimeSpan TimeOfDay { get; private set; }

    public OfficeHour(TimeSpan timeOfDay)
    {
        BusinessException.When(timeOfDay == TimeSpan.Zero, "TimeOfDay cannot be empty");
        TimeOfDay = timeOfDay;
    }
}