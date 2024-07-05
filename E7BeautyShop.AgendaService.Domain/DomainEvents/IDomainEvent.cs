namespace E7BeautyShop.AgendaService.Domain.DomainEvents;

public interface IDomainEvent
{
    DateTime OccuredOn { get; }
}