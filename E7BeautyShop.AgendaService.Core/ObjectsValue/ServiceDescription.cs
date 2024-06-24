using E7BeautyShop.Appointment.Core.Validations;

using static E7BeautyShop.Appointment.Core.Validations.ErrorMessages;

namespace E7BeautyShop.Appointment.Core.ObjectsValue;

public sealed record ServiceDescription
{
    public string? Name { get; }
    public decimal Price { get; }

    public const int MaxNameLength = 150;

    public ServiceDescription(){}
    private ServiceDescription(string? name, decimal price)
    {
        Name = name;
        Price = price;
        Validate();
    }

    private void Validate()
    {
        ArgumentNullException.ThrowIfNull(Name);
        BusinessException.When(Name.Length > MaxNameLength, NameShouldLessThanInformed);
        BusinessException.When(Price <= 0, PriceShouldGreaterThanZero);
    }
    
    public static implicit operator ServiceDescription((string? name, decimal price) tuple)
        => new(tuple.name, tuple.price);
}