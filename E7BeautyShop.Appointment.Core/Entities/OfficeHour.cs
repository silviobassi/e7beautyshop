using E7BeautyShop.Appointment.Core.DomainEvents;
using E7BeautyShop.Appointment.Core.ObjectsValue;
using E7BeautyShop.Appointment.Core.Validations;

namespace E7BeautyShop.Appointment.Core.Entities;

public delegate void DomainEventDelegate(IDomainEvent domainEvent);

public sealed class OfficeHour : Entity
{
    public static readonly int MinimumDuration = 30;
    public DateTime DateAndHour { get; private set; }
    public int Duration { get; private set; }
    public bool IsAvailable { get; set; }
    public Guid? CatalogId { get; init; }
    public Catalog? Catalog { get; private set; }
    public CustomerId? CustomerId { get; private set; }
    public Guid? ScheduleId { get; private set; }
    public event DomainEventDelegate? OnDomainEventOccured;

    private readonly IReservedRegisteredEventFactory? _reservedRegisteredEvent;

    public OfficeHour()
    {
    }

    public OfficeHour(IReservedRegisteredEventFactory reservedRegisteredEvent) => _reservedRegisteredEvent =
        reservedRegisteredEvent ?? throw new ArgumentNullException(nameof(reservedRegisteredEvent));


    private OfficeHour(DateTime dateAndHour, int duration)
    {
        DateAndHour = dateAndHour;
        Duration = duration;
        IsAvailable = true;
        ValidateDateAndHour();
        BusinessException.When(Duration <= 0, "Duration cannot be less than or equal to zero");
    }

    public static OfficeHour Create(DateTime dateAndHour, int duration)
    {
        BusinessException.When(duration < MinimumDuration, $"Duration cannot be less than {MinimumDuration} minutes");
        return new OfficeHour(dateAndHour, duration);
    }

    public void Cancel() => IsAvailable = false;

    public void Attend() => IsAvailable = true;


    public OfficeHour ReserveTimeForTheCustomer(DateTime reserveDateAndHour, CustomerId? customerId, Catalog catalog)
    {
        var officeHour = CreateReserveOfficeHour(reserveDateAndHour, customerId, catalog);
        if (ReserveRegisteredEvent is null)
            throw new InvalidOperationException("Reserved registered event factory is not initialized.");
        OnDomainEventOccured?.Invoke(ReserveRegisteredEvent);
        return officeHour;
    }

    private OfficeHour CreateReserveOfficeHour(DateTime dateAndHour, CustomerId? customer, Catalog catalog)
    {
        DateAndHour = dateAndHour;
        CustomerId = customer;
        IsAvailable = false;
        Catalog = catalog;
        ValidateDateAndHour();
        ValidateCustomer();
        BusinessNullException.When(Catalog is null, nameof(Catalog));
        return this;
    }

    public void ReserveCancel()
    {
        BusinessException.When(IsAvailable, "OfficeHour is already attended");
        ValidateCustomer();
        CustomerId = null;
        Attend();
    }

    public DateTime PlusDuration() => DateAndHour.AddMinutes(Duration);

    private void ValidateDateAndHour() =>
        BusinessNullException.When(DateAndHour == default, nameof(DateAndHour));

    private void ValidateCustomer() =>
        BusinessNullException.When(CustomerId is null, nameof(CustomerId));

    private ReserveRegisteredEvent? ReserveRegisteredEvent => _reservedRegisteredEvent?.Create(CustomerId?.Value,
        DateAndHour, Catalog?.DescriptionName,
        Catalog!.DescriptionPrice.GetValueOrDefault());
}