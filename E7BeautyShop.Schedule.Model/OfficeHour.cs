namespace E7BeautyShop.Schedule;

public sealed class OfficeHour : Appointment
{
    public TimeSpan TimeOfDay { get; private set; }
    public CustomerId? CustomerId { get; private set; }

    public void ReserveTimeForTheCustomer(TimeSpan timeOfDay, CustomerId? customerId)
    {
        TimeOfDay = timeOfDay;
        CustomerId = customerId;
        IsAvailable = false;
        Validate();
    }

    public void  CreateOfficeHour(TimeSpan timeOfDay)
    {
        TimeOfDay = timeOfDay;
        IsAvailable = true;
        Validate();
    }

    public void CustomerCancelled(Guid officeHourId)
    {
        BusinessException.When(IsAvailable, "OfficeHour is already attended");
        BusinessException.When(CustomerId is null, "OfficeHour has no customer");
        CustomerId = null;
        Id = officeHourId;
        Attend();
    }

    private void Validate()
    {
        BusinessException.When(TimeOfDay == TimeSpan.Zero, "TimeOfDay cannot be empty");
    }
}