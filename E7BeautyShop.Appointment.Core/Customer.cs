namespace E7BeautyShop.Appointment.Core;

public sealed class Customer(Guid? id) : Entity
{
    public new Guid? Id { get; private set; } = id;
}