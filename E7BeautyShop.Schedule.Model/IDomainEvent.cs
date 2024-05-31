namespace E7BeautyShop.Schedule;

public interface IDomainEvent
{
    DateTime OccuredOn { get; }
}