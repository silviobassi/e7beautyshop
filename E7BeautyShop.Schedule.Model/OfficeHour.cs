namespace E7BeautyShop.Schedule;

public delegate void DomainEventDelegate(IDomainEvent domainEvent);

public sealed class OfficeHour : Appointment
{
    public event DomainEventDelegate? OnDomainEventOccured;
    private readonly IReservedRegisteredEventFactory _reservedRegisteredEventFactory = new ReserveRegisteredEvent();
    public DateTime ReserveDateAndHour { get; private set; }
    public CustomerId? CustomerId { get; private set; }
    public ServiceCatalog? ServiceCatalog { get; private set; }

    public void ReserveTimeForTheCustomer(DateTime reserveDateAndHour, CustomerId? customerId,
        ServiceCatalog serviceCatalog)
    {
        ReserveDateAndHour = reserveDateAndHour;
        CustomerId = customerId;
        ServiceCatalog = serviceCatalog;
        IsAvailable = false;
        Validate();
        var officeReserveRegisteredEvent =
            _reservedRegisteredEventFactory.Create(CustomerId!.Id, ReserveDateAndHour, serviceCatalog.DescriptionName!,
                serviceCatalog.DescriptionPrice.GetValueOrDefault());
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