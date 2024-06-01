namespace E7BeautyShop.Schedule;

public interface IReservedRegisteredEventFactory
{
    ReserveRegisteredEvent Create(Guid customerId, DateTime reserveDateAndHour,
        string? serviceName, decimal servicePrice);
}

public class ReserveRegisteredEvent : IDomainEvent, IReservedRegisteredEventFactory
{
    public ReserveRegisteredEvent()
    {
    }

    private ReserveRegisteredEvent(Guid customerId, DateTime reserveDateAndHour,
        string? serviceName, decimal servicePrice)
    {
        Id = Guid.NewGuid();
        OccuredOn = DateTime.Now;
        CustomerId = customerId;
        ReserveDateAndHour = reserveDateAndHour;
        ServiceName = serviceName!;
        PriceService = servicePrice;
    }

    public Guid Id { get; set; }

    public DateTime OccuredOn { get; set; }
    public Guid CustomerId { get; set; }
    public DateTime ReserveDateAndHour { get; set; }
    public TimeSpan TimeAttend { get; set; }
    public decimal PriceService { get; set; }
    public string ServiceName { get; set; }

    public ReserveRegisteredEvent Create(Guid customerId, DateTime reserveDateAndHour, string? serviceName,
        decimal servicePrice)
    {
        return new ReserveRegisteredEvent(customerId, reserveDateAndHour, serviceName, servicePrice);
    }
}