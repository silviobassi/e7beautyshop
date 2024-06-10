namespace E7BeautyShop.Appointment.Core;

public delegate void DomainEventDelegate(IDomainEvent domainEvent);

public sealed class OfficeHour : Entity
{
    public DateTime DateAndHour { get; private set; }
    public bool IsAvailable { get; private set; }
    public Guid CatalogId { get; init; }
    public Catalog? Catalog { get; private set; }
    public CustomerId? CustomerId { get; private set; }

    public Guid? ScheduleId { get; private set; }
    public event DomainEventDelegate? OnDomainEventOccured;
    private readonly IReservedRegisteredEventFactory? _reservedRegisteredEvent;

    public OfficeHour()
    {
    }

    public OfficeHour(IReservedRegisteredEventFactory reservedRegisteredEvent)
    {
        _reservedRegisteredEvent =
            reservedRegisteredEvent ?? throw new ArgumentNullException(nameof(reservedRegisteredEvent));
    }

    public void Cancel() => IsAvailable = false;
    public void Attend() => IsAvailable = true;

    public void CreateOfficeHour(DateTime dateAndHour)
    {
        DateAndHour = dateAndHour;
        IsAvailable = true;
        CheckDateAndHour();
    }

    public void ReserveTimeForTheCustomer(DateTime reserveDateAndHour, CustomerId? customerId, Catalog catalog)
    {
        CreateReserveOfficeHour(reserveDateAndHour, customerId, catalog);
        if (ReserveRegisteredEvent is null)
            throw new InvalidOperationException("Reserved registered event factory is not initialized.");
        OnDomainEventOccured?.Invoke(ReserveRegisteredEvent);
    }

    private void CreateReserveOfficeHour(DateTime dateAndHour, CustomerId? customer, Catalog catalog)
    {
        DateAndHour = dateAndHour;
        CustomerId = customer;
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
        CheckDateAndHour();
        BusinessNullException.When(CustomerId is null, nameof(CustomerId));
        BusinessNullException.When(Catalog is null, nameof(Catalog));
    }

    private void CheckDateAndHour() =>
        BusinessNullException.When(DateAndHour == default, nameof(DateAndHour));

    private ReserveRegisteredEvent? ReserveRegisteredEvent => _reservedRegisteredEvent?.Create(CustomerId?.Value,
        DateAndHour, Catalog?.DescriptionName,
        Catalog!.DescriptionPrice.GetValueOrDefault());
}