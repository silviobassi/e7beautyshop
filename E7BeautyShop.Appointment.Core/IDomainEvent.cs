namespace E7BeautyShop.Appointment.Core;

public interface IDomainEvent
{
    DateTime OccuredOn { get; }
}