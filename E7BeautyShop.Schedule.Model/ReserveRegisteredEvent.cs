namespace E7BeautyShop.Schedule;

public class ReserveRegisteredEvent : IDomainEvent
{
    public ReserveRegisteredEvent(Guid newGuid, DateTime occuredOn, Guid customerId, DateTime dateAndHourReserve,
        decimal priceService)
    {
        Id = newGuid;
        OccuredOn = occuredOn;
        CustomerId = customerId;
        DateAndHourReserve = dateAndHourReserve;
        PriceService = priceService;
    }

    public Guid Id { get; set; }

    public DateTime OccuredOn { get; set; }
    public Guid CustomerId { get; set; }
    public DateTime DateAndHourReserve { get; set; }
    public TimeSpan TimeAttend { get; set; }
    public decimal PriceService { get; set; }
}