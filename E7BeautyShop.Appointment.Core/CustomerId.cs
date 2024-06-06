namespace E7BeautyShop.Appointment.Core;

public sealed class CustomerId : Entity
{
    public new Guid? Id { get; private set; }
    public CustomerId(Guid? id)
    {
        Id = id;
    }
}