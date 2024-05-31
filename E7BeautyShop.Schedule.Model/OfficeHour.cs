namespace E7BeautyShop.Schedule;
public delegate void DomainEventDelegate(IDomainEvent domainEvent);
public sealed class OfficeHour : Appointment
{
    public DateTime ReserveDateAndHour { get; private set; }
    public CustomerId? CustomerId { get; private set; }
    public event DomainEventDelegate? OnDomainEventOccured;

    public void ReserveTimeForTheCustomer(DateTime dateAndHour, CustomerId? customerId)
    {
        ReserveDateAndHour = dateAndHour;
        CustomerId = customerId;
        IsAvailable = false;
        Validate();
        var officeReserveRegisteredEvent =
            new ReserveRegisteredEvent(Guid.NewGuid(), DateTime.Now, customerId!.Id, dateAndHour, 10);
        OnDomainEventOccured?.Invoke(officeReserveRegisteredEvent);
    }

    public void CreateOfficeHour(DateTime dateAndHour)
    {
        ReserveDateAndHour = dateAndHour;
        IsAvailable = true;
        Validate();
    }

    public void ReserveCancel(Guid officeHourId)
    {
        BusinessException.When(IsAvailable, "OfficeHour is already attended");
        BusinessException.When(CustomerId is null, "OfficeHour has no customer");
        CustomerId = null;
        Id = officeHourId;
        Attend();
    }
    
    private void Validate()
    {
        BusinessException.When(ReserveDateAndHour.Hour.Equals(0), "TimeOfDay cannot be empty");
    }
}