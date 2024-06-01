namespace E7BeautyShop.Schedule;

public interface IReservedRegisteredEventFactory
{
    ReserveRegisteredEvent Create(Guid customerId, DateTime reserveDateAndHour,
        string? serviceName, decimal servicePrice);
}

public class ReserveRegisteredEvent : IDomainEvent, IReservedRegisteredEventFactory
{
    public Guid Id { get; private set; }

    public DateTime OccuredOn { get; private set; }
    public Guid CustomerId { get; private set; }
    public DateTime ReserveDateAndHour { get; private set; }
    public decimal PriceService { get; private set; }
    public string? ServiceName { get; private set; }

    public ReserveRegisteredEvent Create(Guid customerId, DateTime reserveDateAndHour, string? serviceName,
        decimal servicePrice)
    {
        Id = Guid.NewGuid();
        OccuredOn = DateTime.Now;
        CustomerId = customerId;
        ReserveDateAndHour = reserveDateAndHour;
        ServiceName = serviceName!;
        PriceService = servicePrice;
        return this;
    }
}