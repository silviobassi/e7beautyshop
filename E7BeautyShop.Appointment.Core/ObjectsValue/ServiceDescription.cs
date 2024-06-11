using E7BeautyShop.Appointment.Core.Validations;

namespace E7BeautyShop.Appointment.Core.ObjectsValue;

public sealed record ServiceDescription
{
    public string? Name { get; }
    public decimal Price { get; }

    public const int MaxNameLength = 150;

    private ServiceDescription(string? name, decimal price)
    {
        Name = name;
        Price = price;
        Validate();
    }

    private void Validate()
    {
        BusinessNullException.When(string.IsNullOrEmpty(Name), nameof(Name));
        BusinessException.When(Name?.Length > MaxNameLength, $"Name must be less than {MaxNameLength} characters");
        BusinessException.When(Price <= 0, "Price must be greater than 0");
    }
    
    public static implicit operator ServiceDescription((string? name, decimal price) tuple)
        => new(tuple.name, tuple.price);
}