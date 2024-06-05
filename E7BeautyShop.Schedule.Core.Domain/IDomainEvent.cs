namespace E7BeautyShop.Schedule.Core.Domain;

public interface IDomainEvent
{
    DateTime OccuredOn { get; }
}