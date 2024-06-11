namespace E7BeautyShop.Appointment.Core.DomainEvents;

public interface IDomainEvent
{
    DateTime OccuredOn { get; }
}