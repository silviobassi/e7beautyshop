namespace E7BeautyShop.Appointment.Core;

public sealed class CustomerId
{
    public Guid? Value { get; }

    public CustomerId()
    {
    }

    public CustomerId(Guid value)
    {
        Value = value;
    }
    /*public static implicit operator Guid(Customer customer) => customer.Id;
    public static implicit operator Customer(Guid id) => new(id);*/
}