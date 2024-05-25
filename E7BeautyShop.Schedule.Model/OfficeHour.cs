namespace E7BeautyShop.Schedule;

public sealed class OfficeHour : Appointment
{
    public TimeSpan TimeOfDay { get; private set; }

    public OfficeHour(TimeSpan timeOfDay)
    {
        TimeOfDay = timeOfDay;
        Validate();
    }

    private void Validate() => 
        BusinessException.When(TimeOfDay == TimeSpan.Zero, "TimeOfDay cannot be empty");
}