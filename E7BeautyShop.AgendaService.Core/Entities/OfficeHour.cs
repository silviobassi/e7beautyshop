using E7BeautyShop.AgendaService.Core.DomainEvents;
using E7BeautyShop.Appointment.Core.ObjectsValue;
using E7BeautyShop.Appointment.Core.Validations;
using static E7BeautyShop.Appointment.Core.Validations.ErrorMessages;

namespace E7BeautyShop.AgendaService.Core.Entities;

public delegate void DomainEventDelegate(IDomainEvent domainEvent);

public sealed class OfficeHour : Entity
{
    public const int MinimumDuration = 30;
    public DateTime? DateAndHour { get; private set; }
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


    private OfficeHour(DateTime? dateAndHour, int duration)
    {
        DateAndHour = dateAndHour;
        Duration = duration;
        IsAvailable = true;
        ValidateDateAndHour();
        BusinessException.When(Duration <= 0, DurationCannotLessOrEqualZero);
    }

    public static OfficeHour Create(DateTime? dateAndHour, int durationMinutes)
    {
        BusinessException.When(durationMinutes < MinimumDuration, DurationCannotLessThanInformed);
        return new OfficeHour(dateAndHour, durationMinutes);
    }

    public void Cancel() => IsAvailable = false;

    public void Attend() => IsAvailable = true;


    public OfficeHour ReserveTimeForTheCustomer(DateTime reserveDateAndHour, CustomerId? customerId, Catalog catalog)
    {
        var officeHour = CreateReserveOfficeHour(reserveDateAndHour, customerId, catalog);
        if (ReserveRegisteredEvent is null)
            throw new InvalidOperationException(ReservedRegisteredEventFactoryIsNotInitialized);
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
        ArgumentNullException.ThrowIfNull(nameof(Catalog));
        return this;
    }

    public void ReserveCancel()
    {
        BusinessException.When(IsAvailable, TimeAlreadyAttend);
        ValidateCustomer();
        CustomerId = null;
        Attend();
    }

    public DateTime PlusDuration()
    {
        ValidateDateAndHour();
        return DateAndHour!.Value.AddMinutes(Duration);
    }

    private void ValidateDateAndHour() => ArgumentNullException.ThrowIfNull(DateAndHour);

    private void ValidateCustomer() => ArgumentNullException.ThrowIfNull(CustomerId);

    private ReserveRegisteredEvent? ReserveRegisteredEvent => _reservedRegisteredEvent?.Create(CustomerId?.Value,
        DateAndHour!.Value, Catalog?.DescriptionName,
        Catalog!.DescriptionPrice.GetValueOrDefault());
}