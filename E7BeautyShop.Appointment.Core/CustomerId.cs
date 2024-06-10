namespace E7BeautyShop.Appointment.Core;

public sealed class CustomerId
{
    public Guid? Value { get; }

    public CustomerId()
    {
    }

    private CustomerId(Guid value) => Value = value;

    public static implicit operator CustomerId(Guid value) => new(value);
}