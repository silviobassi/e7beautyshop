namespace E7BeautyShop.Schedule;

public delegate void DomainEventDelegate(IDomainEvent domainEvent);

public sealed class OfficeHour : Appointment
{
    public event DomainEventDelegate? OnDomainEventOccured;
    private readonly IReservedRegisteredEventFactory? _reservedRegisteredEvent;

    public OfficeHour()
    {
    }

    public OfficeHour(IReservedRegisteredEventFactory reservedRegisteredEvent)
    {
        _reservedRegisteredEvent = reservedRegisteredEvent;
    }

    public DateTime DateAndHour { get; private set; }
    public CustomerId? CustomerId { get; private set; }
    public Catalog? Catalog { get; private set; }

    public void CreateOfficeHour(DateTime dateAndHour)
    {
        DateAndHour = dateAndHour;
        IsAvailable = true;
        Validate();
    }

    public void ReserveTimeForTheCustomer(DateTime reserveDateAndHour, CustomerId? customerId, Catalog catalog)
    {
        CreateReserveOfficeHour(reserveDateAndHour, customerId, catalog);
        if (ReserveRegisteredEvent != null) OnDomainEventOccured?.Invoke(ReserveRegisteredEvent);
    }

    private void CreateReserveOfficeHour(DateTime dateAndHour, CustomerId? customerId, Catalog catalog)
    {
        DateAndHour = dateAndHour;
        CustomerId = customerId;
        IsAvailable = false;
        Catalog = catalog;
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
        BusinessException.When(DateAndHour.Hour.Equals(0), "Reserve date and hour cannot be empty");
    }

    private ReserveRegisteredEvent? ReserveRegisteredEvent => _reservedRegisteredEvent?.Create(CustomerId!.Id,
        DateAndHour, Catalog?.DescriptionName!,
        Catalog!.DescriptionPrice.GetValueOrDefault());
}