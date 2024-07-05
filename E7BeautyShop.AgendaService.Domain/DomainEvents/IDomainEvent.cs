namespace E7BeautyShop.AgendaService.Core.DomainEvents;

public interface IDomainEvent
{
    DateTime OccuredOn { get; }
}